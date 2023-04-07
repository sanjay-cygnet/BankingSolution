using AutoMapper;
using Customer.Application.Dtos;
using Customer.Domain.Entities;

namespace Customer.Application.DataMapper
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<Transaction, GetCustomerTransactionDto>();
        }
    }
}
