SQL collation for your tasks

```tsql
ALTER DATABASE [WEIGHT]
COLLATE Cyrillic_General_CI_AS (AI);

SELECT DATABASEPROPERTYEX('SCALES_DB', 'Collation') AS DatabaseCollation;
```
