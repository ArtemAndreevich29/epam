import org.junit.Assert;
import org.junit.Test;

public class WashingMachineFilterTest {

    @Test
    public void testBrandFilter() {
        Assert.assertTrue(WashingMachinePage.filterByBrand("Samsung"));
        Assert.assertTrue(WashingMachinePage.verifyAllProductsBelongToBrand("Samsung"));
    }

    @Test
    public void testPriceFilter() {
        Assert.assertTrue(WashingMachinePage.filterByPriceRange(500, 1000));
        Assert.assertTrue(WashingMachinePage.verifyAllProductsInPriceRange(500, 1000));
    }
@Test
    public void testWashingMachineTypeFilter() {
        Assert.assertTrue(WashingMachinePage.filterByWashingMachineType("Front Load"));
        Assert.assertTrue(WashingMachinePage.verifyAllProductsBelongToType("Front Load"));
    }
@Test
    public void testDepthRangeFilter() {
        Assert.assertTrue(WashingMachinePage.filterByDepthRange("50cm - 60cm"));
        Assert.assertTrue(WashingMachinePage.verifyAllProductsInDepthRange("50cm - 60cm"));
    }
@Test
    public void testResetCharacteristicsOption() 
        WashingMachinePage.selectCharacteristics("Quick Wash", "Energy Star Certified");
        Assert.assertTrue(WashingMachinePage.verifyFilteredProducts());
        Assert.assertTrue(WashingMachinePage.verifyAllProductsDisplayed());
    }
}