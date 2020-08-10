// project=SimTuning.Core, file=TuningOutput.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.Models
{
    public class TuningOutput
    {
        public double? KolbenG { get; set; }
        public double? HubraumV { get; set; }
        public double? ZylinderkopfV { get; set; }
        public double? VerdichtungV { get; set; }
        public double? Pleuelverhaeltnis { get; set; }
        public double? AuspuffResonanzD { get; set; }
        public double? EinlassResonanzD { get; set; }
        public double? MotorMaxD { get; set; }
        public double? EinlassD { get; set; }

        //public double? VenturiVergaserD { get; set; }
        public double? VergaserD { get; set; }

        public double? EinlassA { get; set; }
        public double? Vorauslass { get; set; }
        public double? UeberstroemerD { get; set; }
        public double? UeberstroemerA { get; set; }
        public double? AuslassD { get; set; }
        public double? AuslassA { get; set; }
    }
}