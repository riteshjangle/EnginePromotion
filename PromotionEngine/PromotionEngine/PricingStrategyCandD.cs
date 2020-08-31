using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PricingStrategyCandD :SaleStrategy
    {
        public override Sku Sku { get; } = '&';
        protected override double PricePerOne { get; } = 0 ;
        protected override double PricePerX { get; } = 30;
        protected override int X { get; } = 1;

    }
}
