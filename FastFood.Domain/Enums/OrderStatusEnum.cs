namespace FastFood.Domain.Enums
{
    /// <summary>
    /// Representa o status do pedido.
    /// Received - Pedido recebido.
    /// InPreparation - Pedido em preparação.
    /// Ready - Pedido pronto e aguardando retirada.
    /// Finished - Pedido concluído.
    /// </summary>
    public enum OrderStatusEnum
    {
        Received = 1,
        InPreparation = 2,
        Ready = 3,
        Finished = 4
    }
}
