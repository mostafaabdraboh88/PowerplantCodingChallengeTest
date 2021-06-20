using PowerplantCodingChallengeTest.Core;
using PowerplantCodingChallengeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PowerplantCodingChallengeTest.Models.Powerplant;

namespace PowerplantCodingChallengeTest.Mappers
{
    public static class PayloadMapper
    {
        public static PayloadDto MapPayloadToDto(Payload payload)
        {
            return new PayloadDto() { 
                fuel = new FuelDto() { co2 = payload.fuel.co2, Gas = payload.fuel.Gas , kerosine = payload.fuel.kerosine,wind = payload.fuel.wind},
                Load = payload.Load,
                Powerplants = payload.Powerplants.Select( p => new PowerplantDto() 
                {
                    Efficiency = p.Efficiency , Name = p.Name,Pmin = p.Pmin,
                    Type = (PowerplantDto.PowerplantType)p.Type,
                    Pmax = GetPMax(p.Type, payload.fuel, p.Pmax),
                    MwhPrice = GetMwhPrice(p.Type , payload.fuel , p.Efficiency)
                }).ToList(),
            };
        }

        private static decimal GetPMax(PowerplantType powerplantType, Fuel fuel ,decimal pmax)
        {
            decimal pmaxValue = new decimal();
            switch (powerplantType)
            {
                case PowerplantType.gasfired:
                    pmaxValue = pmax;
                    break;
                case PowerplantType.turbojet:
                    pmaxValue = pmax;
                    break;
                case PowerplantType.windturbine:
                    pmaxValue = pmax * (fuel.wind/ 100);
                    break;
            }
            return pmaxValue;
        }

        private static decimal GetMwhPrice(PowerplantType powerplantType , Fuel fuel , decimal efficiency)
        {
            decimal mwhPrice = new decimal();
            switch (powerplantType)
            {
                case PowerplantType.gasfired :
                    mwhPrice = (fuel.Gas / efficiency) + (fuel.co2 * 0.3m); // Taken into account that a gas-fired powerplant also emits CO2 -  each MWh generated creates 0.3 ton of CO2.
                    break;
                case PowerplantType.turbojet:
                    mwhPrice = fuel.kerosine / efficiency;
                    break;
                case PowerplantType.windturbine:
                    mwhPrice = 0;
                    break;
            }
            return mwhPrice;
        }
    }
}
