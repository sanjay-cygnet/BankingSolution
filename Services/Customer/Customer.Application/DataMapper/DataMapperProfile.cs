namespace Customer.Application.DataMapper;

using AutoMapper;
using Customer.Application.Dtos;
using Customer.Domain.Entities;

public class DataMapperProfile : Profile
{
    public DataMapperProfile()
    {
        CreateMap<Transaction, GetCustomerTransactionDto>();
    }
}
