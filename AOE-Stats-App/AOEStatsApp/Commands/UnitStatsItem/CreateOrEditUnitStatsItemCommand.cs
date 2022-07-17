using AOEStatsApp.Stores;
using AOEStatsApp.ViewModels;
using Domain.Enums;
using Domain.Models;
using AOEStatsApp.Services;
using System;
using System.Threading.Tasks;

namespace AOEStatsApp.Commands
{
    public class CreateOrEditUnitStatsItemCommand : AsyncCommandBase
    {
        private readonly CreateOrEditUnitStatsItemViewModel _unitStatsItemViewModel;
        private readonly UnitStatsStore _unitStatsStore;
        private readonly NotificationsStore _notificationsStore;

        public CreateOrEditUnitStatsItemCommand(CreateOrEditUnitStatsItemViewModel unitStatsItemViewModel,
            UnitStatsStore unitStatsStore,
            NotificationsStore notificationsStore)
        {
            _unitStatsItemViewModel = unitStatsItemViewModel;
            _unitStatsStore = unitStatsStore;
            _notificationsStore = notificationsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _unitStatsItemViewModel.IsLoading = true;

                UnitStatsItem item = new UnitStatsItem()
                {
                    Id = _unitStatsItemViewModel.Id,
                    UnitType = _unitStatsItemViewModel.UnitType,
                    Civilization = _unitStatsItemViewModel.UnitCiviliaztion,
                    Attributes = _unitStatsItemViewModel.Attributes,
                    IsDone = _unitStatsItemViewModel.IsDone,
                    VideoLink = _unitStatsItemViewModel.VideoLink,
                    FoodCost = _unitStatsItemViewModel.UnitFoodCost,
                    WoodCost = _unitStatsItemViewModel.UnitWoodCost,
                    GoldCost = _unitStatsItemViewModel.UnitGoldCost,
                    StoneCost = _unitStatsItemViewModel.UnitStoneCost
                };

                Notification? successNotification;

                if (_unitStatsItemViewModel.IsEditMode)
                {
                    item.DateModified = DateTime.Now;
                    await _unitStatsStore.UpdateUnitStatsItem(item);
                    successNotification = new Notification("Successfully updated unit", MessageType.Success);
                }
                else
                {
                    item.DateCreated = DateTime.Now;
                    await _unitStatsStore.CreateUnitStatsItem(item);
                    _unitStatsItemViewModel.IsEditMode = true;
                    _unitStatsItemViewModel.Id = item.Id;
                    successNotification = new Notification("Successfully created new unit", MessageType.Success);
                }

                _notificationsStore.AddNotification(successNotification);
            }
            catch (Exception ex)
            {
                string message = $"Failed to {(_unitStatsItemViewModel.IsEditMode ? "Update" : "Add")} item";
                var error = new Notification(message, MessageType.Error);
                _notificationsStore.AddNotification(error, ex.ToString());
            }
            finally
            {
                _unitStatsItemViewModel.IsLoading = false;
            }
        }
    }
}