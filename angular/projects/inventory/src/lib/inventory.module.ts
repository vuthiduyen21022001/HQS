import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { InventoryComponent } from './components/inventory.component';
import { InventoryRoutingModule } from './inventory-routing.module';

@NgModule({
  declarations: [InventoryComponent],
  imports: [CoreModule, ThemeSharedModule, InventoryRoutingModule],
  exports: [InventoryComponent],
})
export class InventoryModule {
  static forChild(): ModuleWithProviders<InventoryModule> {
    return {
      ngModule: InventoryModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<InventoryModule> {
    return new LazyModuleFactory(InventoryModule.forChild());
  }
}
