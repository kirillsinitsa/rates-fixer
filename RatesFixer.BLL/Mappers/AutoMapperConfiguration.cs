using AutoMapper;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;

namespace RatesFixer.BLL.Mappers
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration VMtoEntity
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FactoryFloorVM, FactoryFloor>();
                    cfg.CreateMap<DivisionVM, Division>();
                    cfg.CreateMap<WorkStationVM, WorkStation>();
                    cfg.CreateMap<EmployeeVM, Employee>();
                    cfg.CreateMap<TeamVM, Team>();
                    cfg.CreateMap<OperationVM, Operation>();
                    cfg.CreateMap<TariffMultiplierVM, TariffMultiplier>();
                    cfg.CreateMap<TariffPayGroupVM, TariffPayGroup>();
                    cfg.CreateMap<PartOperationVM, PartOperation>();
                    cfg.CreateMap<PartVM, Part>();
                    cfg.CreateMap<TariffPayVM, TariffPay>();
                    cfg.CreateMap<WageRateVM, WageRate>();
                    cfg.CreateMap<OrderVM, Order>();
                    cfg.CreateMap<OrderEntryVM, OrderEntry>();
                    cfg.CreateMap<PaymentVM, Payment>();
                });
            }
        }

        private static MapperConfiguration EntityToVM
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FactoryFloor, FactoryFloorVM>();
                    cfg.CreateMap<Division, DivisionVM>();
                    cfg.CreateMap<WorkStation, WorkStationVM>();
                    cfg.CreateMap<Employee, EmployeeVM>().PreserveReferences();
                    cfg.CreateMap<Team, TeamVM>().PreserveReferences();
                    cfg.CreateMap<Operation, OperationVM>();
                    cfg.CreateMap<TariffMultiplier, TariffMultiplierVM>();
                    cfg.CreateMap<TariffPayGroup, TariffPayGroupVM>();
                    cfg.CreateMap<PartOperation, PartOperationVM>();
                    cfg.CreateMap<Part, PartVM>();
                    cfg.CreateMap<TariffPay, TariffPayVM>();
                    cfg.CreateMap<WageRate, WageRateVM>();
                    cfg.CreateMap<Order, OrderVM>();
                    cfg.CreateMap<OrderEntry, OrderEntryVM>();
                    cfg.CreateMap<Payment, PaymentVM>();
                });
            }
        }

        public static MapperConfiguration FactoryFloorVMToFactoryFloor
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration FactoryFloorToFactoryFloorVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration DivisionVMToDivision
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration DivisionToDivisionVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration WorkStationVMToWorkStation
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration WorkStationToWorkStationVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration EmployeeVMToEmployee
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration TeamToTeamVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration TeamVMToTeam
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration EmployeeToEmployeeVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration OperationVMToOperation
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration OperationToOperationVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration TariffMultiplierVMToTariffMultiplier
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration TariffMultiplierToTariffMultiplierVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration TariffPayGroupVMToTariffPayGroup
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration TariffPayGroupToTariffPayGroupVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration PartOperationVMToPartOperation
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration PartOperationToPartOperationVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration PartVMToPart
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration PartToPartVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration TariffPayVMToTariffPay
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration TariffPayToTariffPayVM
        {
            get => EntityToVM;
        }

        public static MapperConfiguration WageRateVMToWageRate
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration WageRateToWageRateVM
        {
            get => EntityToVM;
        }
        public static MapperConfiguration OrderVMToOrder
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration OrderToOrderVM
        {
            get => EntityToVM;
        }
        public static MapperConfiguration OrderEntryVMToOrderEntry
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration OrderEntryToOrderEntryVM
        {
            get => EntityToVM;
        }
        public static MapperConfiguration PaymentVMToPayment
        {
            get => VMtoEntity;
        }

        public static MapperConfiguration PaymentToPaymentVM
        {
            get => EntityToVM;
        }
    }
}
