using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Steppenwolf.Pages.Admin.Category;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Admin
{
    public class ManageCategoryBase : ComponentBase
    {
        protected IList<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        
        protected CategoryModel CategoryModel { get; set; } = new CategoryModel();
        
        private Contracts.Category Category { get; set; }
        
        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        private CategoryService CategoryService { get; set; }
        
        [Inject]
        private IMapper Mapper { get; set; }
        
        protected override async void OnInitialized()
        {
            await this.SetInitialData();
            this.StateHasChanged();
        }

        protected async Task Submit()
        {
            var category = this.Mapper.Map(this.CategoryModel, this.Category);
            
            await this.CategoryService.UpsertAsync(category);
            await this.SetInitialData();
        }
        
        protected async Task Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return;
            }
            
            await this.CategoryService.DeleteAsync(id.Value);
            await this.SetInitialData();
        }
        
        protected async Task SelectForUpdate(Guid? id)
        {
            if (!id.HasValue)
            {
                return;
            }

            var existingCategory = await this.CategoryService.GetByIdAsync(id.Value);
            this.CategoryModel = this.Mapper.Map<CategoryModel>(existingCategory);
        }

        private async Task SetInitialData()
        {
            var categories = await this.CategoryService.GetAllAsync(1, 10);

            this.Categories = this.Mapper.Map<IList<CategoryModel>>(categories);
            this.CategoryModel = new CategoryModel();
            this.Category = null;
        }
    }
}