﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;
using WeightCore.Helpers;
using WeightCore.MassaK;

namespace WeightCore.Managers
{
    public class MassaManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MassaManagerHelper _instance;
        public static MassaManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties - Manager

        public int WaitReopen { get; private set; }
        public int WaitRequest { get; private set; }
        public int WaitResponse { get; private set; }
        public int WaitException { get; private set; }
        public int WaitClose { get; private set; }
        public string ExceptionMsg { get; private set; }

        #endregion

        #region Public fields and properties

        private readonly MassaRequestHelper _massaRequest = MassaRequestHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public decimal WeightNet { get; private set; }
        public decimal WeightGross { get; private set; }
        public byte IsStable { get; private set; }
        public int ScaleFactor { get; set; } = 1_000;
        public int CurrentError { get; private set; }
        public ResponseParseEntity ResponseParseScalePar { get; private set; } = null;
        public ResponseParseEntity ResponseParseGet { get; private set; } = null;
        public ResponseParseEntity ResponseParseSet { get; private set; } = null;
        public bool IsResponse { get; private set; }
        public BlockingCollection<MassaExchangeEntity> Requests { get; private set; } = new();
        public MassaDeviceEntity MassaDevice { get; private set; }
        public bool IsInit { get; private set; }

        #endregion

        #region Constructor and destructor

        public MassaManagerHelper()
        {

        }

        public void Init(string portName, int readTimeout, int writeTimeout, 
            int waitResponse = 500, int waitRequest = 250, int waitReopen = 5_000, int waitClose = 5_000, int waitException = 1_000)
        {
            if (IsInit)
                return;
            IsInit = true;

            WaitResponse = waitResponse == 0 ? 500 : waitResponse;
            WaitRequest = waitRequest == 0 ? 250 : waitRequest;
            WaitReopen = waitReopen == 0 ? 5_000 : waitReopen;
            WaitClose = waitClose == 0 ? 5_000 : waitClose;
            WaitException = waitException == 0 ? 5_000 : waitException;
            MassaDevice = new(portName, readTimeout, writeTimeout);
        }

        #endregion

        #region Public and private methods - Manager

        public void OpenResponse()
        {
            // Clear requests.
            if (Requests.Count > 100)
            {
                Requests = new BlockingCollection<MassaExchangeEntity>();
                return;
            }

            if (MassaDevice.IsConnected)
            {
                foreach (MassaExchangeEntity massaExchange in Requests.GetConsumingEnumerable())
                {
                    if (MassaDevice == null || massaExchange == null) return;
                    Parse(massaExchange);
                }
                Requests = new BlockingCollection<MassaExchangeEntity>();
            }
        }

        public void Close()
        {
            try
            {
                CloseJob();
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        #endregion

        #region Public and private methods

        private void Parse(MassaExchangeEntity massaExchange)
        {
            switch (massaExchange.CmdType)
            {
                case MassaCmdType.UdpPoll:
                    massaExchange.Request = _massaRequest.CMD_UDP_POLL;
                    break;
                case MassaCmdType.GetInit2:
                    massaExchange.Request = _massaRequest.CMD_GET_INIT_2;
                    break;
                case MassaCmdType.GetInit3:
                    massaExchange.Request = _massaRequest.CMD_GET_INIT_3;
                    break;
                case MassaCmdType.GetEthernet:
                    massaExchange.Request = _massaRequest.CMD_GET_ETHERNET;
                    break;
                case MassaCmdType.GetWiFiIp:
                    massaExchange.Request = _massaRequest.CMD_GET_WIFI_IP;
                    break;
                case MassaCmdType.GetMassa:
                    massaExchange.Request = _massaRequest.CMD_GET_MASSA;
                    break;
                case MassaCmdType.GetName:
                    break;
                case MassaCmdType.GetScalePar:
                    massaExchange.Request = _massaRequest.CMD_GET_SCALE_PAR;
                    break;
                case MassaCmdType.GetScaleParAfter:
                    massaExchange.Request = _massaRequest.CMD_GET_SCALE_PAR_AFTER;
                    break;
                case MassaCmdType.SetTare:
                    massaExchange.Request = massaExchange.CmdSetTare();
                    break;
                case MassaCmdType.SetZero:
                    massaExchange.Request = _massaRequest.CMD_SET_ZERO;
                    break;
            }
            if (massaExchange.Request == null)
                return;

            byte[] response = MassaDevice.WriteToPort(massaExchange);
            IsResponse = response != null;
            if (!IsResponse)
                return;

            massaExchange.ResponseParse = new ResponseParseEntity(massaExchange.CmdType, response);
            ParseSetResponse(massaExchange);
            ParseSetMassa(massaExchange);
        }

        private void ParseSetResponse(MassaExchangeEntity massaExchange)
        {
            switch (massaExchange.CmdType)
            {
                case MassaCmdType.GetMassa:
                    ResponseParseGet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.GetScalePar:
                    ResponseParseScalePar = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetWiFiSsid:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetDatetime:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetName:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetRegnum:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetTare:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetZero:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
            }
        }

        private void ParseSetMassa(MassaExchangeEntity massaExchange)
        {
            switch (massaExchange.CmdType)
            {
                case MassaCmdType.GetMassa:
                    // 1 байт. Цена деления в значении массы нетто и массы тары:
                    // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                    ScaleFactor = massaExchange.ResponseParse.Massa.ScaleFactor;
                    // 4 байта. Текущая масса нетто со знаком
                    WeightNet = massaExchange.ResponseParse.Massa.Weight / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    decimal weightTare = massaExchange.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    WeightGross = WeightNet + weightTare;
                    // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                    IsStable = massaExchange.ResponseParse.Massa.Stable;
                    // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                    //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                    break;
            }
        }

        public void CloseJob()
        {
            MassaDevice?.Dispose();
        }

        public void GetInit()
        {
            GetInit1();
            GetInit2();
            GetInit3();

            GetScalePar();
            GetScaleParAfter();
            GetScalePar();
            GetMassa();

            SetZero();
            SetTareWeight(0);
            SetZero();

            //RequestQueue.CompleteAdding();
        }

        public void GetInit1() => Requests.Add(new MassaExchangeEntity(MassaCmdType.UdpPoll));
        public void GetInit2() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetInit2));
        public void GetInit3() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetInit3));
        public void GetMassa() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetMassa));

        public void GetScalePar() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetScalePar));
        public void GetScaleParAfter() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetScaleParAfter));
        public void SetTareWeight(int weightTare) => Requests.Add(new MassaExchangeEntity(MassaCmdType.SetTare, weightTare));
        public void SetZero() => Requests.Add(new MassaExchangeEntity(MassaCmdType.SetZero));

        #endregion
    }
}
