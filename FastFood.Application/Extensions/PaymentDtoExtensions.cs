using FastFood.Application.Dtos.Payment;
using FastFood.Domain.Entities;

namespace FastFood.Application.Extensions;

public static class PaymentDtoExtensions
{
    public static Payment ToEntity(this PaymentDto dto, string method, DateTime paymentDate, long? paymentIdMP, int orderId, int paymentStatusId)
    {
        return new Payment(
            paymentIdMP: paymentIdMP,
            price: dto.Price.ToString("F2"),
            method: method,
            paymentDate: paymentDate,
            orderId: orderId,
            paymentStatusId: paymentStatusId
        );
    }
}