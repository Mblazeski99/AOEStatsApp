using AOEStatsApp.Commands;
using AOEStatsApp.Stores;
using AOEStatsApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Domain.Enums;
using System.Linq;

namespace AOEStatsApp.ViewModels
{
    public class CreateOrEditUnitStatsItemViewModel : ViewModelBase
    {
        public int Id { get; set; }

        private Unit _unitType;
        public Unit UnitType
        {
            get { return _unitType; }
            set
            {
                _unitType = value;
                OnPropertyChanged(nameof(UnitType));
            }
        }

        private Civilization _unitCiviliaztion;
        public Civilization UnitCiviliaztion
        {
            get { return _unitCiviliaztion; }
            set
            {
                _unitCiviliaztion = value;
                OnPropertyChanged(nameof(UnitCiviliaztion));
            }
        }

        private string? _attributes;
        public string? Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                OnPropertyChanged(nameof(Attributes));
            }
        }

        private string? _videoLink;
        public string? VideoLink
        {
            get { return _videoLink; }
            set
            {
                _videoLink = value;
                OnPropertyChanged(nameof(VideoLink));
            }
        }

        private double _unitFoodCost;
        public double UnitFoodCost
        {
            get { return _unitFoodCost; }
            set
            {
                _unitFoodCost = value;
                OnPropertyChanged(nameof(UnitFoodCost));
            }
        }

        private double _unitWoodCost;
        public double UnitWoodCost
        {
            get { return _unitWoodCost; }
            set
            {
                _unitWoodCost = value;
                OnPropertyChanged(nameof(UnitWoodCost));
            }
        }

        private double _unitGoldCost;
        public double UnitGoldCost
        {
            get { return _unitGoldCost; }
            set
            {
                _unitGoldCost = value;
                OnPropertyChanged(nameof(UnitGoldCost));
            }
        }

        private double _unitStoneCost;
        public double UnitStoneCost
        {
            get { return _unitStoneCost; }
            set
            {
                _unitStoneCost = value;
                OnPropertyChanged(nameof(UnitStoneCost));
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set 
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(EnableInput));
            }
        }

        public bool EnableInput => !IsLoading;

        public bool IsEditMode { get; set; }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateOrEditUnitStatsItemViewModel(UnitStatsStore unitStatsStore, NavigationService<UnitStatsItemListingViewModel> navigationService, NotificationsStore notificationsStore)
        {
            if (unitStatsStore.CurrentUnitStatsItem != null)
            {
                IsEditMode = true;
                Id = unitStatsStore.CurrentUnitStatsItem.Id;
                UnitType = unitStatsStore.CurrentUnitStatsItem.UnitType;
                UnitCiviliaztion = unitStatsStore.CurrentUnitStatsItem.Civilization;
                Attributes = unitStatsStore.CurrentUnitStatsItem.Attributes;
                VideoLink = unitStatsStore.CurrentUnitStatsItem.VideoLink;
                UnitFoodCost = unitStatsStore.CurrentUnitStatsItem.FoodCost;
                UnitWoodCost = unitStatsStore.CurrentUnitStatsItem.WoodCost;
                UnitGoldCost = unitStatsStore.CurrentUnitStatsItem.GoldCost;
                UnitStoneCost = unitStatsStore.CurrentUnitStatsItem.StoneCost;
                IsDone = unitStatsStore.CurrentUnitStatsItem.IsDone;
            }

            SubmitCommand = new CreateOrEditUnitStatsItemCommand(this, unitStatsStore, notificationsStore);
            CancelCommand = new NavigateCommand<UnitStatsItemListingViewModel>(navigationService);
        }
    }
}