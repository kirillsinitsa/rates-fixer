using Prism.Events;
using RatesFixer.BLL.Models;
using System.Collections.Generic;

namespace RatesFixer
{
    public class FactoryFloorCreatedEvent : PubSubEvent<FactoryFloorVM> { }
    public class FactoryFloorChangedEvent : PubSubEvent<FactoryFloorVM> { }
    public class FactoryFloorDeletedEvent : PubSubEvent<int> { }
    public class DivisionCreatedEvent : PubSubEvent<DivisionVM> { }
    public class DivisionChangedEvent : PubSubEvent<DivisionVM> { }
    public class DivisionDeletedEvent : PubSubEvent<int> { }
    public class WorkStationCreatedEvent : PubSubEvent<WorkStationVM> { }
    public class WorkStationChangedEvent : PubSubEvent<WorkStationVM> { }
    public class WorkStationDeletedEvent : PubSubEvent<int> { }
    public class EmployeeCreatedEvent : PubSubEvent<EmployeeVM> { }
    public class EmployeeChangedEvent : PubSubEvent<EmployeeVM> { }
    public class EmployeeDeletedEvent : PubSubEvent<int> { }
    public class OperationCreatedEvent : PubSubEvent<OperationVM> { }
    public class OperationChangedEvent : PubSubEvent<OperationVM> { }
    public class OperationDeletedEvent : PubSubEvent<int> { }
    public class PartOperationCreatedEvent : PubSubEvent<PartOperationVM> { }
    public class PartOperationChangedEvent : PubSubEvent<PartOperationVM> { }
    public class PartOperationDeletedEvent : PubSubEvent<int> { }
    public class PartCreatedEvent : PubSubEvent<PartVM> { }
    public class PartChangedEvent : PubSubEvent<PartVM> { }
    public class PartDeletedEvent : PubSubEvent<int> { }
    public class TariffMultiplierCreatedEvent : PubSubEvent<TariffMultiplierVM> { }
    public class TariffMultiplierChangedEvent : PubSubEvent<TariffMultiplierVM> { }
    public class TariffMultiplierDeletedEvent : PubSubEvent<int> { }
    public class TariffPaysChangedEvent : PubSubEvent<List<TariffPayVM>> { }
    public class TariffPayGroupCreatedEvent : PubSubEvent<TariffPayGroupVM> { }
    public class TariffPayGroupChangedEvent : PubSubEvent<TariffPayGroupVM> { }
    public class TariffPayGroupDeletedEvent : PubSubEvent<int> { }
    public class OrderCreatedEvent : PubSubEvent<OrderVM> { }
    public class OrderChangedEvent : PubSubEvent<OrderVM> { }
    public class OrderDeletedEvent : PubSubEvent<int> { }
    public class TeamCreatedEvent : PubSubEvent<TeamVM> { }
    public class TeamChangedEvent : PubSubEvent<TeamVM> { }
    public class TeamDeletedEvent : PubSubEvent<int> { }
    public class WageRateCreatedEvent : PubSubEvent<WageRateVM> { }
    public class WageRateChangedEvent : PubSubEvent<WageRateVM> { }
    public class WageRateDeletedEvent : PubSubEvent<int> { }
    public class OrdersClosedEvent : PubSubEvent { }
    public class ApplicationClosedEvent : PubSubEvent { }
}
