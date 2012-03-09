
using System.Diagnostics;
namespace Disney.iDash.SRR.BusinessLayer
{
    public class SummaryItem
    {
        public decimal HierarchyKey { get; set; }
        public decimal Department { get; set; }
        public decimal Class { get; set; }
        public string Market { get; set; }
        public decimal MarketColour { get; set; }
        public string MarketSequence { get; set; }
        public string Grade { get; set; }
        public string GradeDescription { get; set; }
        public decimal Store { get; set; }
        public string StoreName { get; set; }
        public string KeyValue { get; set; }
        public string StoreType { get; set; }
        public string WorkBench { get; set; }
        
        public decimal? UpliftFactor { get; set; }
        public decimal UpliftInheritedLevel { get; set; }
        public bool UpliftActualFlag { get; set; }
        public decimal UpliftOverrideLevel { get; set; }
        public string UpliftStatus { get; set; }
        
        public decimal? CutOff { get; set; }
        public decimal CutOffInheritedLevel { get; set; }
        public bool CutOffActualFlag { get; set; }
        public decimal CutOffOverrideLevel { get; set; }
        public string CutOffStatus { get; set; }
        
        public string Allocation { get; set; }
        public decimal AllocationInheritedLevel { get; set; }
        public bool AllocationActualFlag { get; set; }
        public decimal AllocationOverrideLevel { get; set; }
        public string AllocationStatus { get; set; }
        
        public decimal? SmoothFactor { get; set; }
        public decimal SmoothFactorInheritedLevel { get; set; }
        public bool SmoothFactorActualFlag { get; set; }
        public decimal SmoothFactorOverrideLevel { get; set; }
        public string SmoothFactorStatus { get; set; }
        
        public decimal? Pattern { get; set; }
        public decimal PatternInheritedLevel { get; set; }
        public bool PatternActualFlag { get; set; }
        public decimal PatternOverrideLevel { get; set; }
        public string PatternStatus { get; set; }

        // Replenishment Values
        public decimal StockRequirement { get; set; }
        public decimal IdealReplenishmentQuantity { get; set; }
        public decimal ProposedAllocationQuantity { get; set; }

        public string Changed { get; set; }

    }
}
