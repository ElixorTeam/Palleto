// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocalizationLabelPrint : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationLabelPrint()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\LabelPrint.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }

    #endregion

    #region CodingSeb.Localization

    public string AlreadyRunning => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AlreadyRunning)}");
    public string AppExit => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppExit)}");
    public string AppExitDescription => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppExitDescription)}");
    public string AppLoad => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppLoad)}");
    public string AppLoadDescription => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppLoadDescription)}");
    public string AppTitle => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppTitle)}");
    public string AppWait => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppWait)}");
    public string Bundle => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Bundle)}");
    public string ButtonAddKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonAddKneading)}");
    public string ButtonPlu => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonPlu)}");
    public string ButtonScalesInit => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInit)}");
    public string ButtonScalesInitShort => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInitShort)}");
    public string ButtonSelectOrder => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInitShort)}");
    public string ButtonSetKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonSetKneading)}");
    public string ButtonSettings => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonSettings)}");
    public string CheckAllPassed => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckAllPassed)}");
    public string CheckPluError => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckPluError)}");
    public string CheckPluWeightCount => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckPluWeightCount)}");
    public string CheckWeightIsEmpty() => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsEmpty)}");
    public string CheckWeightIsZero => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsZero)}");
    public string CommunicateWithAdmin => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CommunicateWithAdmin)}");
    public string ComPort => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string ComPortState => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string Crc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Crc)}");
    public string Default => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Default)}");
    public string DeviceControlIsPreview => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(DeviceControlIsPreview)}");
    public string Exception => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string ExceptionSqlDb => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string FieldCurrentTime => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldCurrentTime)}");
    public string FieldDate => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldDate)}");
    public string FieldGln => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldGln)}");
    public string FieldIsIncrementCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounter)}");
    public string FieldIsIncrementCounterEnable => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounterEnable)}");
    public string FieldKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldKneading)}");
    public string FieldLabelCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldLabelCounter)}");
    public string FieldPalletSize => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldPalletSize)}");
    public string FieldPrintCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldPrintCounter)}");
    public string FieldProductDate => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldProductDate)}");
    public string FieldSscc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSscc)}");
    public string FieldSsccControlNumber => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSsccControlNumber)}");
    public string FieldSynonym => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSynonym)}");
    public string FieldThresholdLower => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdLower)}");
    public string FieldThresholdNominal => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdNominal)}");
    public string FieldThresholds => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholds)}");
    public string FieldThresholdUpper => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdUpper)}");
    public string FieldTime => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldTime)}");
    public string FieldUnitId => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldUnitId)}");
    public string FieldUnitType => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldUnitType)}");
    public string FieldWeightNetto => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldWeightNetto)}");
    public string FieldWeightTare => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldWeightTare)}");
    public string HostUidNotFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToDb => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToFile => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidQuestionWriteToFile)}");
    public string IsConnectWithMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsConnectWithMassa)}");
    public string IsDataNotExists => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsDataNotExists)}");
    public string IsNotConnectWithMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsNotConnectWithMassa)}");
    public string LabelContextExpirationDt => Lang == WsEnumLanguage.English ? "Good to" : "Годен до";
    public string LabelContextKneading => Lang == WsEnumLanguage.English ? "Kneading" : "Замес";
    public string LabelContextNesting => Lang == WsEnumLanguage.English ? "Nesting" : "Вложенность";
    public string LabelContextPlu => Lang == WsEnumLanguage.English ? "PLU" : "ПЛУ";
    public string LabelContextProductDt => Lang == WsEnumLanguage.English ? "Date of production" : "Дата изготовления";
    public string LabelContextWeight => Lang == WsEnumLanguage.English ? "Weight" : "Вес";
    public string LabelContextWorkShop => Lang == WsEnumLanguage.English ? "WorkShop/Line" : "Цех/Линия";
    public string LabelPrint => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelPrint)}");
    public string Labels => Lang == WsEnumLanguage.English ? "Labels" : "Этикетки";
    public string Line => Lang == WsEnumLanguage.English ? "Line" : "Строка";
    public string MassaDevice => Lang == WsEnumLanguage.English ? "Massa-K" : "Масса-К";
    public string MassaExchange => Lang == WsEnumLanguage.English ? "Massa exchange" : "Масса обмен";
    public string MassaIsNotCalc => Lang == WsEnumLanguage.English ? "The weight has not been calculated!" : "Вес не расчитан!";
    public string MassaIsNotFound => Lang == WsEnumLanguage.English ? "The scales has not been found!" : "Весы не обнаружены!";
    public string MassaIsNotQuering => Lang == WsEnumLanguage.English ? "The scales are not respond!" : "Весы не отвечают!";
    public string MassaIsNotRespond => Lang == WsEnumLanguage.English ? "The scales Massa-K are not respond!" : "Весы Масса-К не отвечают!";
    public string MassaK => Lang == WsEnumLanguage.English ? "Scales Massa-K" : "Весы Масса-К";
    public string MassaManager => Lang == WsEnumLanguage.English ? "Massa manager" : "Менеджер массы";
    public string MassaWaitStable => Lang == WsEnumLanguage.English ? "Wait for the weight platform to stabilize." : "Дождитесь стабилизации весовой платформы.";
    public string Memory => Lang == WsEnumLanguage.English ? "Memory" : "Память";
    public string MemoryAll => Lang == WsEnumLanguage.English ? "all" : "всего";
    public string MemoryBusy => Lang == WsEnumLanguage.English ? "busy" : "занято";
    public string MemoryFree => Lang == WsEnumLanguage.English ? "free" : "свободно";
    public string MemoryPhysical => Lang == WsEnumLanguage.English ? "Physical memory" : "Физическая память";
    public string MemoryVirtual => Lang == WsEnumLanguage.English ? "Virtual memory" : "Виртуальная память";
    public string Message => Lang == WsEnumLanguage.English ? "Message" : "Сообщение";
    public string Method => Lang == WsEnumLanguage.English ? "Method" : "Метод";
    public string OperationControl => Lang == WsEnumLanguage.English ? "Control of operations" : "Контроль операций";
    public string PackagedInModifiedAtmosphere => Lang == WsEnumLanguage.English ? "Packaged in modified atmosphere" : "Упаковано в модифицированной атмосфере";
    public string Plu => Lang == WsEnumLanguage.English ? "PLU" : "ПЛУ";
    public string PluCode => Lang == WsEnumLanguage.English ? "Code" : "Код";
    public string PluCodeNotSet => Lang == WsEnumLanguage.English ? "Code is not set" : "Код не задан";
    public string PluCount => Lang == WsEnumLanguage.English ? "PLU (count)" : "ПЛУ (шт)";
    public string PluCountNesting(short nesting) => Lang == WsEnumLanguage.English ? $"PLU ({nesting} count)" : $"ПЛУ ({nesting} шт)";
    public string PluDescriptionNotSet => Lang == WsEnumLanguage.English ? "Descr is not set" : "Описание не задано";
    public string PluDescriptionSet => Lang == WsEnumLanguage.English ? "Descr is not set" : "Описание задано";
    public string PluEan13IsNotSet => Lang == WsEnumLanguage.English ? "EAN13 is not set" : "ЕАН13 не задан";
    public string PluginDefault => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginDefault)}");
    public string PluginLabel => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginLabel)}");
    public string PluginMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginMassa)}");
    public string PluginMemory => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginMemory)}");
    public string PluginPrint => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrint)}");
    public string PluginPrintTsc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrintTsc)}");
    public string PluginPrintZebra => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrintZebra)}");
    public string PluGtin => Lang == WsEnumLanguage.English ? "GTIN" : "ГТИН";
    public string PluGtinIsNotSet => Lang == WsEnumLanguage.English ? "GTIN is not set" : "ГТИН не задан";
    public string PluIsPiece => Lang == WsEnumLanguage.English ? "pcs." : "шт";
    public string PluIsWeight => Lang == WsEnumLanguage.English ? "weight" : "вес";
    public string PluItf14IsNotSet => Lang == WsEnumLanguage.English ? "ITF14 is not set" : "ИТФ14 не задан";
    public string PluNotSelect => Lang == WsEnumLanguage.English ? "PLU is not selected!" : "ПЛУ не выбрана!";
    public string PluNotSelectWeight => Lang == WsEnumLanguage.English ? "Weight PLU is not selected!" : "Весовая ПЛУ не выбрана!";
    public string PluPackageNotSelect => Lang == WsEnumLanguage.English ? "PLU nesting is not selected!" : "Тара ПЛУ не выбрана!";
    public string PluPage => Lang == WsEnumLanguage.English ? "Page" : "Страница";
    public string PluTemplateIsNotSet => Lang == WsEnumLanguage.English ? "PLU template is not set" : "Шаблон не задан";
    public string PluTemplateNotSet => Lang == WsEnumLanguage.English ? "Template is not set!" : "Шаблон не задан!";
    public string PluTemplateSet => Lang == WsEnumLanguage.English ? "Template is not set" : "Шаблон задан";
    public string PluWeight => Lang == WsEnumLanguage.English ? "PLU (weight)" : "ПЛУ (вес)";
    public string ProgramExit => Lang == WsEnumLanguage.English ? "Ending the program ..." : "Завершение программы ...";
    public string ProgramLoad => Lang == WsEnumLanguage.English ? "Loading the program ..." : "Загрузка программы ...";
    public string QuestionCloseApp => Lang == WsEnumLanguage.English ? "Close the app" : "Завершить приложение";
    public string QuestionPerformOperation => Lang == WsEnumLanguage.English ? "Perform the operation?" : "Выполнить операцию?";
    public string QuestionRunApp => Lang == WsEnumLanguage.English ? "Run the app" : "Запустить приложение";
    public string QuestionWriteToDb => Lang == WsEnumLanguage.English ? "Add new record into the DB?" : "Добавить новую запись в БД?";
    public string Registration => Lang == WsEnumLanguage.English ? "Device registration" : "Регистрация устройства";
    public string RequestParameters => Lang == WsEnumLanguage.English ? "Request parameters" : "Запрос параметров";
    public string Restore => Lang == WsEnumLanguage.English ? "Restore" : "Восстановить";
    public string RestoreDevice => Lang == WsEnumLanguage.English ? "Restore device" : "Восстановить устроство";
    public string ScaleQueue => Lang == WsEnumLanguage.English ? "Scales message queue" : "Очередь сообщений весов";
    public string ScheduleForNextDay => Lang == WsEnumLanguage.English ? "Schedule for the next day" : "Расписание на следующий день";
    public string ScheduleForNextHour => Lang == WsEnumLanguage.English ? "Schedule for the next hour" : "Расписание на следующий час";
    public string ScreenResolution => Lang == WsEnumLanguage.English ? "Screen resolution" : "Разрешение экрана";
    public string ShippingLabels => Lang == WsEnumLanguage.English ? "Shipping labels" : "Транспортные этикетки";
    public string StateCorrect => Lang == WsEnumLanguage.English ? "correct" : "верна";
    public string StateDisable => Lang == WsEnumLanguage.English ? "disable" : "отключено";
    public string StateError => Lang == WsEnumLanguage.English ? "error" : "ошибка";
    public string StateIsNotResponsed => Lang == WsEnumLanguage.English ? "is not responsed" : "нет ответа";
    public string StateIsResponsed => Lang == WsEnumLanguage.English ? "is responsed" : "есть ответ";
    public string SwitchKneadingTitle => Lang == WsEnumLanguage.English ? "Switch kneading for" : "Смена замеса для";
    public string SwitchLineTitle => Lang == WsEnumLanguage.English ? "Switch Line" : "Смена линии";
    public string SwitchMoreTitle => Lang == WsEnumLanguage.English ? "Switch more" : "Смена ещё";
    public string SwitchPluLineTitle => Lang == WsEnumLanguage.English ? "Switch line's PLU" : "Смена ПЛУ линии";
    public string SwitchPluNestingTitle => Lang == WsEnumLanguage.English ? "Switch PLU nesting" : "Смена вложенности ПЛУ";
    public string SwitchPluTitle => Lang == WsEnumLanguage.English ? "Switch PLU" : "Смена ПЛУ";
    public string ThreadId => "ID";
    public string ThreadIsBackground => Lang == WsEnumLanguage.English ? "Is background" : "Фоновый";
    public string ThreadName => Lang == WsEnumLanguage.English ? "Name" : "Имя";
    public string ThreadPriorityLevel => Lang == WsEnumLanguage.English ? "Priority level" : "Приоритет";
    public string Threads => Lang == WsEnumLanguage.English ? "Threads" : "Потоки";
    public string ThreadsCount => Lang == WsEnumLanguage.English ? "Threads count" : "Количество потоков";
    public string ThreadStartTime => Lang == WsEnumLanguage.English ? "Start time" : "Время запуска";
    public string ThreadState => Lang == WsEnumLanguage.English ? "State" : "Состояние";
    public string UnitWeight => Lang == WsEnumLanguage.English ? "weight" : "вес";
    public string WaitAppComplete => Lang == WsEnumLanguage.English ? "Wait for application to complete" : "Ожидайте завершения приложения";
    public string WeightingControl => Lang == WsEnumLanguage.English ? "The weight is out of bounds!" : "Вес выходит за границы!";
    public string WeightingIsCalc => Lang == WsEnumLanguage.English ? "Stable is calculated" : "Рассчитывается вес";
    public string WeightingIsStableDescription => Lang == WsEnumLanguage.English ? "Scales are stable | Gross weight" : "Весы стабильны | Вес брутто";
    public string WeightingIsStableFlag => Lang == WsEnumLanguage.English ? "Stable" : "Стабилизация";
    public string WeightingIsStableNo => Lang == WsEnumLanguage.English ? "Scale are not stable" : "Весы не стабильны";
    public string WeightingIsStableYes => Lang == WsEnumLanguage.English ? "Scale are stable" : "Весы стабильны";
    public string WeightingMessage => Lang == WsEnumLanguage.English ? "Weighting message" : "Сообщение взвешивания";
    public string WeightingProcess => Lang == WsEnumLanguage.English ? "Weighing | Gross weight" : "Взвешивание | Вес брутто";
    public string WeightingScaleCmd => Lang == WsEnumLanguage.English ? "Command for scales" : "Команда для весов";
    public string WeightUnitGr => Lang == WsEnumLanguage.English ? "gr" : "гр";
    public string WeightUnitKg => Lang == WsEnumLanguage.English ? "kg" : "кг";
    public string WeightUnitPcs => Lang == WsEnumLanguage.English ? "pcs." : "шт.";

    #endregion

    #region Deprecated

    public string ButtonNewPallet => Lang == WsEnumLanguage.English ? $"New{Environment.NewLine}pallet" : $"Новая{Environment.NewLine}палета";
    public string ButtonRunScalesTerminal => Lang == WsEnumLanguage.English ? $"Scales{Environment.NewLine}Terminal" : $"Весовой{Environment.NewLine}терминал";
    public string ButtonScaleChange(int number) => Lang == WsEnumLanguage.English ? $"Change ARM [{number}]" : $"Сменить АРМ [{number}]";
    public string ButtonSelectPlu(int count) => Lang == WsEnumLanguage.English ? $"PLU{Environment.NewLine}({count} pieces)" : $"ПЛУ{Environment.NewLine}({count} шт.)";
    public string CheckWeightBefore(decimal currentWeight) => Lang == WsEnumLanguage.English ? "Unload the weight platform!" + Environment.NewLine + Environment.NewLine + $"Threshold value: {WsLocalizationUtils.MassaThresholdValue:0.000} {WeightUnitKg}." + Environment.NewLine + $"Current gross value: {currentWeight:0.000} {WeightUnitKg}." : "Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine + $"Пороговое значение: {WsLocalizationUtils.MassaThresholdValue:0.000} {WeightUnitKg}." + Environment.NewLine + $"Текущее значение брутто: {currentWeight:0.000} {WeightUnitKg}.";
    public string CheckWeightThreshold(decimal weightNet) => Lang == WsEnumLanguage.English ? $"{WeightingControl} Product weight: {weightNet:0.000} {WeightUnitKg}" : $"{WeightingControl} Вес продукта: {weightNet:0.000} {WeightUnitKg}";
    public string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal nominalWeight, decimal lowerWeightThreshold) => Lang == WsEnumLanguage.English ? $"{WeightingControl} Net weight: {currentNet:0.000} {WeightUnitKg} Upper value: {upperWeightThreshold:0.000} {WeightUnitKg} Nominal value: {nominalWeight:0.000} {WeightUnitKg} Lower value: {lowerWeightThreshold:0.000} {WeightUnitKg}" : $"{WeightingControl} Вес нетто: {currentNet:0.000} {WeightUnitKg} Верхнее значение: {upperWeightThreshold:0.000} {WeightUnitKg} Номинальное значение: {nominalWeight:0.000} {WeightUnitKg} Нижнее значение: {lowerWeightThreshold:0.000} {WeightUnitKg}";
    public string HostNotFound(string deviceName) => Lang == WsEnumLanguage.English ? $"Host '{deviceName}' not detected." : $"Хост '{deviceName}' не обнаружен.";
    public string HostQuestionWriteToDb(string deviceName) => Lang == WsEnumLanguage.English ? $"Add new host '{deviceName}' into the DB?" : $"Добавить новый хост '{deviceName}' в БД?";
    public string IsException(string? message) => Lang == WsEnumLanguage.English ? $"Error! {message}" : $"Ошибка! {message}";
    public string ProgramNotFound(string fileName) => Lang == WsEnumLanguage.English ? "Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator." : "Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
    public string RegistrationSuccess(string deviceName, string scaleName) => Lang == WsEnumLanguage.English ? $"Host '{deviceName}' and ARM '{scaleName}' are found." : $"Хост '{deviceName}' и АРМ '{scaleName}' найдены.";
    public string RegistrationWarning(Guid uid) => Lang == WsEnumLanguage.English ? $"Host UID: {uid}. Before restarting, map the host to an ARM in the DeviceControl application." : $"УИД хоста: {uid}. Перед повторным запуском, сопоставьте хост с АРМом в приложении DeviceControl.";
    public string RegistrationWarningHostNotFound(string deviceName) => Lang == WsEnumLanguage.English ? $"Host '{deviceName}' not found!" : $"Хост '{deviceName}' не найден!";
    public string RegistrationWarningLineNotFound(string deviceName) => Lang == WsEnumLanguage.English ? $"Line for host '{deviceName}' not found!" : $"Линия для хоста '{deviceName}' не найдена!";
    public string SetArea(long id, string name) => Lang == WsEnumLanguage.English ? $"Switch area: {id} | {name}" : $"Смена площадки: {id} | {name}";
    public string SetLine(long id, string name) => Lang == WsEnumLanguage.English ? $"Switch line: {id} | {name}" : $"Смена линии: {id} | {name}";
    public string SetPlu(int number, string name) => Lang == WsEnumLanguage.English ? $"Switch PLU: {number} | {name}" : $"Смена ПЛУ: {number} | {name}";
    public string SetPluNesting(int number, string name, short bundleCount) => Lang == WsEnumLanguage.English ? $"Switch PLU nesting: {number} | {name} | {bundleCount}" : $"Смена вложенности ПЛУ: {number} | {name} | {bundleCount}";

    #endregion
}