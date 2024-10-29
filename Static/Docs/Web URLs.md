# Web URLs
kolbasa-vs.local

Debug
Data Source=CREATIO\INS1; Initial Catalog=SCALES; Persist Security Info=False; Integrated Security=False; User ID=scale01; Password=scale01; TrustServerCertificate=True;

Release
Data Source=PALYCH\LUTON; Initial Catalog=ScalesDB; Persist Security Info=False; Integrated Security=False; User ID=scale01; Password=scale01; TrustServerCertificate=True;


## IIS-сервера
IIS-DEV (10.0.204.17)			DIISM04 | ISEXCD04
IIS-PROD (10.0.24.19)			DIISM01 | ISEXCD01
CREATIO (10.0.24.24)			DIISM02 | ISEXCD02
PALYCH (10.0.204.24)			SCALES

## SQL-сервера
CREATIO\INS1 (10.0.24.24)		CREATIO, SCALES, VSDWH
PALYCH\LUTON (10.0.204.24)		ScalesDB
SQLSRSP01\LEEDS (10.0.204.15)	VSDWH
DEV1C\DVLP (10.0.204.20)		1C-PROD

## Веб-сервис "Обмен с весовыми постами" / WsWebApiScales
✓ IIS-DEV (10.0.204.17)
Предварительное тестирование	https://scales-dev-preview.kolbasa-vs.local:443/
Рабочее тестирование			https://scales-dev.kolbasa-vs.local:443/
✓ IIS: IIS-PROD (10.0.24.19)
Предварительный релиз			https://scales-prod-preview.kolbasa-vs.local:443/
Рабочий релиз					https://scales-prod.kolbasa-vs.local:443/

## Веб-сервис "Обмен с DWH" / WebApiTerra1000
✓ IIS-DEV (10.0.204.17)
Предварительное тестирование	https://t1000-dev-preview.kolbasa-vs.local:443/
Рабочее тестирование			https://t1000-dev.kolbasa-vs.local:443/
✓ IIS: IIS-PROD (10.0.24.19)
Предварительный релиз			https://t1000-preview.kolbasa-vs.local:443/
Рабочий релиз					https://t1000.kolbasa-vs.local:443/

## Веб-приложение "Управление устройствами" / DeviceControl
✓ IIS: IIS-DEV (10.0.204.17)
Предварительное тестирование	https://device-control-dev-preview.kolbasa-vs.local/
Рабочее тестирование			https://device-control-dev.kolbasa-vs.local/
✓ IIS: IIS-PROD (10.0.24.19)
Предварительный релиз			https://device-control-prod-preview.kolbasa-vs.local/
Рабочий релиз					https://device-control-prod.kolbasa-vs.local/
Рабочий релиз					https://device-control.kolbasa-vs.local/
