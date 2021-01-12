﻿namespace SimTuning.Test
{
    public interface IViewModelTest
    {
        #region Auslass

        void AuslassAnwendungViewModelTest();

        void AuslassMainViewModelTest();

        void AuslassTheorieViewModelTest();

        #endregion Auslass

        #region Dyno

        void DynoAudioPlayerViewModelTest();

        void DynoAusrollenViewModelTest();

        void DynoBeschleunigungViewModelTest();

        void DynoDataViewModelTest();

        void DynoDiagnosisViewModelTest();

        void DynoMainViewModelTest();

        void DynoRuntimeViewModelTest();

        void DynoSpectrogramViewModelTest();

        #endregion Dyno

        #region Einlass

        void EinlassKanalViewModelTest();

        void EinlassMainViewModelTest();

        void EinlassVergaserViewModelTest();

        #endregion Einlass

        #region Motor

        void MotorHubraumViewModelTest();

        void MotorMainViewModelTest();

        void MotorSteuerdiagrammViewModelTest();

        void MotorUmrechnungViewModelTest();

        void MotorVerdichtungViewModelTest();

        #endregion Motor

        #region Einstellungen

        void EinstellungenApplicationViewModelTest();

        void EinstellungenAussehenViewModelTest();

        void EinstellungenMenuViewModelTest();

        void EinstellungenVehiclesViewModelTest();

        #endregion Einstellungen

        void DemoMainViewModelTest();

        void HomeHomeViewModelViewModelTest();

        void MainPageViewModelTest();

        void MenuViewModelTest();
    }
}