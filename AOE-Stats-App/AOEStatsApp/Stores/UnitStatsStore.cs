using Domain.Models;
using AOEStatsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOEStatsApp.ViewModels;

namespace AOEStatsApp.Stores
{
    public class UnitStatsStore
    {
        private readonly IUnitStatsItemService _unitStatsItemService;
        private readonly List<UnitStatsItem> _unitStatsItems;
        private Lazy<Task> _initializeLazy;

        public UnitStatsItem? CurrentUnitStatsItem { get; private set; }

        public IEnumerable<UnitStatsItem> UnitStatsItems => _unitStatsItems;

        public event Action ItemsUpdated;

        public UnitStatsStore(IUnitStatsItemService unitStatsItemService)
        {
            _unitStatsItemService = unitStatsItemService;

            _initializeLazy = new Lazy<Task>(Initialize);
            _unitStatsItems = new List<UnitStatsItem>();
        }

        public async Task CreateUnitStatsItem(UnitStatsItem item)
        {
            await _unitStatsItemService.CreateUnitStatsItem(item);
            OnItemsUpdated();
        }

        public async Task UpdateUnitStatsItem(UnitStatsItem item)
        {
            await _unitStatsItemService.UpdateUnitStatsItem(item);
            OnItemsUpdated();
        }

        public async Task DeleteUnitStatsItem(UnitStatsItem item)
        {
            await _unitStatsItemService.DeleteUnitStatsItem(item);
            OnItemsUpdated();
        }

        public void SetCurrentUnitStatsItem(UnitStatsItem? item)
        {
            CurrentUnitStatsItem = item;
        }

        private async Task OnItemsUpdated()
        {
            await Initialize();
            ItemsUpdated?.Invoke();
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception ex)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        private async Task Initialize()
        {
            IEnumerable<UnitStatsItem> items = await _unitStatsItemService.GetAllItems();

            _unitStatsItems.Clear();
            _unitStatsItems.AddRange(items);
        }
    }
}
