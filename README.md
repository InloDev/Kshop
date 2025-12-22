The project is a store template. It will be built using modularity and a domain-driven design (DDD) in .NET.

## Соглашения по коду

### Именование переменных и аргументов

- **Запрещено** использовать однобуквенные переменные или аргументы
- В лямбда-выражениях используйте осмысленные имена вместо `x`, `p`, `i` и т.д.
  - ❌ `products.Where(p => p.Price > 100)`
  - ✅ `products.Where(product => product.Price > 100)`
  - ✅ `products.Where(item => item.Price > 100)`

### Работа с decimal

- Всегда используйте суффикс `M` для decimal литералов
  - ❌ `decimal price = 100;`
  - ❌ `decimal price = 100.0;`
  - ✅ `decimal price = 100M;`
  - ✅ `new Price(100M, CurrencyCode.USD)`

## Соглашения по тестированию

### Наименование тестов

Используется формат: `{MethodName}_{Scenario}_{ExpectedResult}`

Примеры:

- `Create_ValidArguments_Success` - успешное создание через конструктор
- `Create_NullName_ThrowsArgumentException` - проверка валидации в конструкторе
- `CreateProduct_ValidData_ReturnsNewInstance` - фабричный метод
- `Update_ValidArguments_UpdatesProperties` - успешное обновление
- `Calculate_NegativeAmount_ThrowsInvalidOperationException` - бизнес-правило

Для Theory тестов с несколькими случаями используйте обобщенное название:

- `Create_InvalidName_ThrowsArgumentException` с `[InlineData(null)]`, `[InlineData("")]`

Специальные случаи:

- Для конструкторов используйте просто `Create_`
- Для статических фабричных методов используйте полное имя метода
- Группируйте связанные тесты во вложенные классы для лучшей организации