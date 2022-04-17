using System;
using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.ViewModels.Order;
using MotoStore.ViewModels.Positions;

namespace MotoStore.MapConfigurations
{
    public static class PositionMapConfiguration
    {
        public static Position MapToPosition(this CreatePositionViewModel model)
        {
            return new Position
            {
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                EngineCapacity = model.EngineCapacity,
                CreationDateTime = DateTime.Now,
                NumberOfCycles = model.NumberOfCycles
            };
        }

        public static List<PositionShortInfoViewModel> MapToShortInfoList(this List<Position> positions)
        {
            return positions.Select(position => 
                    new PositionShortInfoViewModel
                    {
                        Id = position.Id, 
                        Manufacturer = position.Manufacturer, 
                        Model = position.Model,
                        PathToImage = position.PathToImage
                    }).ToList();
        }
        
        public static PositionInfoViewModel MapToPositionInfoViewModel(this Position model)
        {
            return new PositionInfoViewModel
            {
                Id = model.Id,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                EngineCapacity = model.EngineCapacity,
                NumberOfCycles = model.NumberOfCycles,
                PathToImage = model.PathToImage
            };
        }

        public static Order MapToOrder(this OrderCreateViewModel model)
        {
            Order order = new Order
            {
                CreationDateTime = DateTime.Now,
                PositionId = model.PositionId
            };
            return order;
        }
    }
}