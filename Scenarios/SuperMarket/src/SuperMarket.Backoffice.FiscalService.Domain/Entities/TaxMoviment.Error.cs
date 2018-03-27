namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public partial class TaxMoviment
    {
        public enum Error
        {
            TaxMovimentMustHaveOrderBaseValue,
            TaxMovimentMustHaveOrderDiscount,
            TaxMovimentMustHaveOrderId
        }
    }
}
