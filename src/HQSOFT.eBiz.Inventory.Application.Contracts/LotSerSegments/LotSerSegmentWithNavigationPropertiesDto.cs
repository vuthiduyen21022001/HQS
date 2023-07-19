using HQSOFT.eBiz.Inventory.LotSerClasses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventory.LotSerSegments
{
    public class LotSerSegmentWithNavigationPropertiesDto
    {
        public LotSerSegmentDto LotSerSegment { get; set; }

        public LotSerClassDto LotSerClass { get; set; }

    }
}