namespace WsLocalizationCore.Models;

public sealed class WsLocaleTable : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string AccessLevel => Lang == WsEnumLanguage.English ? "Access level" : "Уровень доступа";
    public string Address => Lang == WsEnumLanguage.English ? "Address" : "Адрес";
    public string App => Lang == WsEnumLanguage.English ? "Program" : "Программа";
    public string Area => Lang == WsEnumLanguage.English ? "Area" : "Площадка";
    public string Box => Lang == WsEnumLanguage.English ? "Box" : "Коробка";
    public string BoxWeight => Lang == WsEnumLanguage.English ? "Box weight" : "Вес коробки";
    public string Brand => Lang == WsEnumLanguage.English ? "Brand" : "Бренд";
    public string Bundle => Lang == WsEnumLanguage.English ? "Bundle" : "Пакет";
    public string BundleFkWeightTare => Lang == WsEnumLanguage.English ? "Bundle tare weight" : "Вес тары упаковки";
    public string BundleWeight => Lang == WsEnumLanguage.English ? "Bundle weight" : "Вес пакета";
    public string CategoryName => Lang == WsEnumLanguage.English ? "Category" : "Категория";
    public string ChangeDt => Lang == WsEnumLanguage.English ? "Edited" : "Изменено";
    public string CheckWeight => Lang == WsEnumLanguage.English ? "Weighing products" : "Весовая продукция";
    public string Code => Lang == WsEnumLanguage.English ? "Code" : "Код";
    public string Count => Lang == WsEnumLanguage.English ? "Count" : "Кол-во";
    public string Counter => Lang == WsEnumLanguage.English ? "Counter" : "Счётчик";
    public string CreateDt => Lang == WsEnumLanguage.English ? "Created" : "Создано";
    public string DayOfWeek => Lang == WsEnumLanguage.English ? "Weekday" : "День недели";
    public string Description => Lang == WsEnumLanguage.English ? "Description" : "Описание";
    public string Device => Lang == WsEnumLanguage.English ? "Device" : "Устройство";
    public string Devices => Lang == WsEnumLanguage.English ? "Devices" : "Устройства";
    public string Setting => Lang == WsEnumLanguage.English ? "Setting" : "Настройка";
    public string DeviceComPort => Lang == WsEnumLanguage.English ? "COM-port" : "COM-порт";
    public string DeviceIp => Lang == WsEnumLanguage.English ? "IP-address" : "IP-адрес";
    public string DeviceMac => Lang == WsEnumLanguage.English ? "MAC-address" : "MAC-адрес";
    public string DeviceNumber => Lang == WsEnumLanguage.English ? "Device number" : "Номер устройства";
    public string DeviceType => Lang == WsEnumLanguage.English ? "Device type" : "Тип устройства";
    public string Ean13 => Lang == WsEnumLanguage.English ? "EAN13" : "ШК ЕАН13";
    public string Error => Lang == WsEnumLanguage.English ? "Failed" : "Неудачно";
    public string ExpirationDt => Lang == WsEnumLanguage.English ? "Expiration date" : "Срок годности";
    public string FieldEmpty => Lang == WsEnumLanguage.English ? "<Empty>" : "<Пусто>";
    public string FieldPluIsNotSelected => Lang == WsEnumLanguage.English ? "PLU is not selected!" : "ПЛУ не выбрана!";
    public string File => Lang == WsEnumLanguage.English ? "File" : "Файл";
    public string FullName => Lang == WsEnumLanguage.English ? "Full name" : "Полное наименование";
    public string Gln => Lang == WsEnumLanguage.English ? "GLN" : "ГЛН";
    public string Gtin => Lang == WsEnumLanguage.English ? "BC GTIN" : "ШК ГТИН";
    public string Host => Lang == WsEnumLanguage.English ? "Host" : "Хост";
    public string Id => Lang == WsEnumLanguage.English ? "ID" : "ИД";
    public string ImageData => Lang == WsEnumLanguage.English ? "Image data" : "Данные";
    public string IsDefault => Lang == WsEnumLanguage.English ? "Default" : "По-умолчанию";
    public string IsEnabled => Lang == WsEnumLanguage.English ? "Is enabled" : "Включено";
    public string IsKneading => Lang == WsEnumLanguage.English ? "Kneading" : "Замес";
    public string IsMarked => Lang == WsEnumLanguage.English ? "In the archive" : "В архиве";
    public string IsMarkedShort => Lang == WsEnumLanguage.English ? "x" : "х";
    public string Itf14 => "ITF14";
    public string Label => Lang == WsEnumLanguage.English ? "Label" : "Этикетка";
    public string LabelCounter => Lang == WsEnumLanguage.English ? "Label counter" : "Счётчик этикеток";
    public string Line => Lang == WsEnumLanguage.English ? "Line" : "Линия";
    public string LoginDt => Lang == WsEnumLanguage.English ? "Login" : "Залогирован";
    public string LogoutDt => Lang == WsEnumLanguage.English ? "Logout" : "Разлогирован";
    public string LogType => Lang == WsEnumLanguage.English ? "Log type" : "Тип лога";
    public string Member => Lang == WsEnumLanguage.English ? "Method" : "Метод";
    public string Message => Lang == WsEnumLanguage.English ? "Message" : "Сообщение";
    public string Name => Lang == WsEnumLanguage.English ? "Name" : "Наименование";
    public string NestingCount => Lang == WsEnumLanguage.English ? "Nesting count" : "Кол-во вложений";
    public string NestingMeasurement => Lang == WsEnumLanguage.English ? "pc" : "шт";
    public string NomenclatureCode => Lang == WsEnumLanguage.English ? "Nomenclature code" : "Код номенклатуры";
    public string Number => Lang == WsEnumLanguage.English ? "Number" : "Номер";
    public string NumberShort => Lang == WsEnumLanguage.English ? "#" : "№";
    public string Percents => Lang == WsEnumLanguage.English ? "Percents" : "Проценты";
    public string Plu => Lang == WsEnumLanguage.English ? "PLU" : "ПЛУ";
    public string PluBundleFk => Lang == WsEnumLanguage.English ? "PLU's bundle" : "Пакет ПЛУ";
    public string PluNesting => Lang == WsEnumLanguage.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string PluNumber => Lang == WsEnumLanguage.English ? "# PLU" : "№ ПЛУ";
    public string PluStorage => Lang == WsEnumLanguage.English ? "Storage PLU" : "Способ хранения ПЛУ";
    public string PrettyName => Lang == WsEnumLanguage.English ? "Pretty name" : "Красивое наименование";
    public string Printer => Lang == WsEnumLanguage.English ? "Printer" : "Принтер";
    public string ProductDt => Lang == WsEnumLanguage.English ? "Product date" : "Дата продукции";
    public string ProductionFacility => Lang == WsEnumLanguage.English ? "Production facility" : "Производственная площадка";
    public string ReleaseDt => Lang == WsEnumLanguage.English ? "Release date" : "Дата релиза";
    public string Request => Lang == WsEnumLanguage.English ? "Request" : "Запрос";
    public string RequestUrl => Lang == WsEnumLanguage.English ? "Request url" : "Url запроса";
    public string Response => Lang == WsEnumLanguage.English ? "Response" : "Ответ";
    public string Row => Lang == WsEnumLanguage.English ? "Row" : "Строка";
    public string RowCount => Lang == WsEnumLanguage.English ? "Row count" : "Кол-во записей";
    public string Schema => Lang == WsEnumLanguage.English ? "Schema" : "Схема";
    public string ShelfLifeDays => Lang == WsEnumLanguage.English ? "Shelf life, days" : "Срок годности, суток";
    public string ShelfLifeDaysShort => Lang == WsEnumLanguage.English ? "Life" : "Срок";
    public string Size => Lang == WsEnumLanguage.English ? "Size mb" : "Размер в мб";
    public string Success => Lang == WsEnumLanguage.English ? "Success" : "Успешно";
    public string Table => Lang == WsEnumLanguage.English ? "Table" : "Таблица";
    public string TableCount => Lang == WsEnumLanguage.English ? "Table count" : "Кол-во таблиц";
    public string TableDelete => Lang == WsEnumLanguage.English ? "Delete record" : "Удалить запись";
    public string TableMark => Lang == WsEnumLanguage.English ? "Mark record" : "Пометить запись на удаление";
    public string TableNew => Lang == WsEnumLanguage.English ? "New record" : "Новая запись";
    public string TableSave => Lang == WsEnumLanguage.English ? "Save record" : "Сохранить запись";
    public string Template => Lang == WsEnumLanguage.English ? "Template" : "Шаблон";
    public string TemplateLabelary => Lang == WsEnumLanguage.English ? "Web-site Labelary" : "Веб-сайт Labelary";
    public string TempMaximal => Lang == WsEnumLanguage.English ? "Maximal weight" : "Максимальная температура";
    public string TempMinimal => Lang == WsEnumLanguage.English ? "Minimal temperature" : "Минимальная температура";
    public string Title => Lang == WsEnumLanguage.English ? "Title" : "Заголовок";
    public string Type => Lang == WsEnumLanguage.English ? "Type" : "Тип";
    public string TypeBottom => Lang == WsEnumLanguage.English ? "Bottom's type" : "Нижний тип";
    public string TypeRight => Lang == WsEnumLanguage.English ? "Right's type" : "Правый тип";
    public string TypeTop => Lang == WsEnumLanguage.English ? "Top's type" : "Верхний тип";
    public string Uid => Lang == WsEnumLanguage.English ? "UID" : "УИД";
    public string Uid1c => Lang == WsEnumLanguage.English ? "UID 1C" : "УИД 1C";
    public string User => Lang == WsEnumLanguage.English ? "User" : "Пользователь";
    public string ValueBottom => Lang == WsEnumLanguage.English ? "Bottom's value" : "Нижнее значение";
    public string ValueRight => Lang == WsEnumLanguage.English ? "Right's value" : "Правое значение";
    public string ValueTop => Lang == WsEnumLanguage.English ? "Top's value" : "Верхнее значение";
    public string Version => Lang == WsEnumLanguage.English ? "Version" : "Версия";
    public string Weighing => Lang == WsEnumLanguage.English ? "Weighing" : "Взвешивание";
    public string Weight => Lang == WsEnumLanguage.English ? "Weight" : "Вес";
    public string Weighted => Lang == WsEnumLanguage.English ? "Weighted" : "Весовая";
    public string WeightMaximal => Lang == WsEnumLanguage.English ? "Maximal weight" : "Максимальный вес";
    public string WeightMinimal => Lang == WsEnumLanguage.English ? "Minimal weight" : "Минимальный вес";
    public string WeightNetto => Lang == WsEnumLanguage.English ? "Net weight" : "Вес нетто";
    public string WeightNominal => Lang == WsEnumLanguage.English ? "Nominal weight" : "Номинальный вес";
    public string WeightShort => Lang == WsEnumLanguage.English ? "Weight" : "Вес";
    public string WeightTare => Lang == WsEnumLanguage.English ? "Tare weight" : "Вес тары";
    public string WorkShop => Lang == WsEnumLanguage.English ? "Workshop" : "Цех";
    public string WorkShopName => Lang == WsEnumLanguage.English ? "Workshop" : "Цех";
    public string WithoutWeightCount => Lang == WsEnumLanguage.English ? "Count" : "Кол-во шт";
    public string WeightCount => Lang == WsEnumLanguage.English ? "Count" : "Кол-во вес";
    public string DownloadUrl => "Каталог установки";
    
    #endregion
}