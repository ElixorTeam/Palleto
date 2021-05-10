USE [ScalesDB]
GO

DECLARE @ip nvarchar(100) = '10.0.20.67'
DECLARE @port int = 9100
DECLARE @zplCommand nvarchar(max) =
'^XA
^CI28
^CWK,E:COURB.TTF
^CWL,E:COURBI.TTF
^CWM,E:COURBD.TTF
^CWN,E:COUR.TTF
^CWZ,E:ARIAL.TTF
^CWW,E:ARIALBI.TTF
^CWE,E:ARIALBD.TTF
^CWR,E:ARIALI.TTF

^LH0,10
^FWR

^LL1180
^PW944

^FO820,50
^CFZ,24,20
^FB1100,4,0,C,0
^FH^FD_d0_98_d0_b7_d0_b3_d0_be_d1_82_d0_be_d0_b2_d0_b8_d1_82_d0_b5_d0_bb_d1_8c: _d0_9e_d0_9e_d0_9e "_d0_92_d0_bb_d0_b0_d0_b4_d0_b8_d0_bc_d0_b8_d1_80_d1_81_d0_ba_d0_b8_d0_b9 _d1_81_d1_82_d0_b0_d0_bd_d0_b4_d0_b0_d1_80_d1_82" _d0_a0_d0_be_d1_81_d1_81_d0_b8_d1_8f, 600910 _d0_92_d0_bb_d0_b0_d0_b4_d0_b8_d0_bc_d0_b8_d1_80_d1_81_d0_ba_d0_b0_d1_8f _d0_be_d0_b1_d0_bb. _d0_b3._d0_a0_d0_b0_d0_b4_d1_83_d0_b6_d0_bd_d1_8b_d0_b9 _d0_ba_d0_b2_d0_b0_d1_80_d1_82_d0_b0_d0_bb 13/13 _d0_b4_d0_be_d0_bc 20^FS
^FO510,50
^CFE,44,34
^FB910,4,0,J,0
^FH^FD_d0_98_d0_b7_d0_b4_d0_b5_d0_bb_d0_b8_d1_8f _d0_ba_d0_be_d0_bb_d0_b1_d0_b0_d1_81_d0_bd_d1_8b_d0_b5 _d0_b2_d0_b0_d1_80_d0_b5_d0_bd_d0_be-_d0_ba_d0_be_d0_bf_d1_87_d0_b5_d0_bd_d1_8b_d0_b5. _d0_9f_d1_80_d0_be_d0_b4_d1_83_d0_ba_d1_82 _d0_bc_d1_8f_d1_81_d0_bd_d0_be_d0_b9 _d0_ba_d0_b0_d1_82_d0_b5_d0_b3_d0_be_d1_80_d0_b8_d0_b8 _d0_92. _d0_9a_d0_be_d0_bb_d0_b1_d0_b0_d1_81_d0_b0 _d0_b2_d0_b0_d1_80_d0_b5_d0_bd_d0_be-_d0_ba_d0_be_d0_bf_d1_87_d0_b5_d0_bd_d0_b0_d1_8f "_d0_a1_d0_b5_d1_80_d0_b2_d0_b5_d0_bb_d0_b0_d1_82 _d0_9a_d0_be_d0_bd_d1_8c_d1_8f_d1_87_d0_bd_d1_8b_d0_b9" _d0_be_d1_85_d0_bb_d0_b0_d0_b6_d0_b4_d0_b5_d0_bd_d0_bd_d0_b0_d1_8f _d0_a2_d0_a3 10.13.14-003-91005552-2015^FS
^FO350,50
^CFZ,36,20
^FB800,4,0,J,0
^FH^FD_d0_a1_d1_80_d0_be_d0_ba _d0_b3_d0_be_d0_b4_d0_bd_d0_be_d1_81_d1_82_d0_b8: 30 _d1_81_d1_83_d1_82_d0_be_d0_ba _d0_bf_d1_80_d0_b8 _d1_82_d0_b5_d0_bc_d0_bf_d0_b5_d1_80_d0_b0_d1_82_d1_83_d1_80_d0_b5 _d0_be_d1_82 0_c2_b0_d0_a1 _d0_b4_d0_be +6_c2_b0_d0_a1 _d0_b8 _d0_be_d1_82_d0_bd_d0_be_d1_81_d0_b8_d1_82_d0_b5_d0_bb_d1_8c_d0_bd_d0_be_d0_b9 _d0_b2_d0_bb_d0_b0_d0_b6_d0_bd_d0_be_d1_81_d1_82_d0_b8 _d0_b2_d0_be_d0_b7_d0_b4_d1_83_d1_85_d0_b0 75%-78%. _d0_a3_d0_bf_d0_b0_d0_ba_d0_be_d0_b2_d0_b0_d0_bd_d0_be _d0_bf_d0_be_d0_b4 _d0_b2_d0_b0_d0_ba_d1_83_d1_83_d0_bc_d0_be_d0_bc.^FS
^FO320,50
^CFZ,25,20
^FB270,1,0,L,0
^FH^FD_d0_94_d0_b0_d1_82_d0_b0 _d0_b8_d0_b7_d0_b3_d0_be_d1_82_d0_be_d0_b2_d0_bb_d0_b5_d0_bd_d0_b8_d1_8f: ^FS
^FO270,50
^CFK,56,40
^FB300,1,0,L,0
^FH^FD06.10.2020^FS
^FO320,360
^CFZ,25,20
^FB270,1,0,L,0
^FH^FD_d0_93_d0_be_d0_b4_d0_b5_d0_bd _d0_b4_d0_be: ^FS
^FO270,360
^CFK,56,40
^FB300,1,0,L,0
^FH^FD05.11.2020^FS
^FO320,700
^CFZ,25,20
^FB100,1,0,L,0
^FH^FD_d0_92_d0_b5_d1_81 _d0_bd_d0_b5_d1_82_d1_82_d0_be: ^FS
^FO270,700
^CFK,56,38
^FB230,1,0,L,0
^FH^FD1.205^FS
^FO270,815
^CFK,56,38
^FB100,1,0,L,0
^FH^FD_d0_ba_d0_b3^FS
^FO200,50
^CFZ,25,20
^FB200,1,0,L,0
^FH^FD_d0_97_d0_b0_d0_bc_d0_b5_d1_81: 1^FS
^FO200,200
^CFZ,25,20
^FB450,1,0,L,0
^FH^FD_d0_a6_d0_b5_d1_85/_d0_9b_d0_b8_d0_bd_d0_b8_d1_8f: Line 13 (_d0_9e_d0_9f01)^FS

^FO200,1000
^BCN,120,Y,N,N
^BY2
^FD2990001300016456^FS

^FO740,50
^BY2
^BCR,120,Y,N,N

^FD298000130001645620100614480813001205001^FS

^FO50,50
^BCR,120,Y,N,Y,D
^BY3
^FD(01)02600108000001(3103)001205(11)201006(10)1006>8^FS

^FO200,888
^XGE:EAC.GRF,1,1^FS
^FO315,888
^XGE:FISH.GRF,1,1^FS
^FO435,888
^XGE:TEMP6.GRF,1,1^FS

^XZ';

EXECUTE [db_scales].[ZplPipe] 
   @ip
  ,@port
  ,@zplCommand
GO
