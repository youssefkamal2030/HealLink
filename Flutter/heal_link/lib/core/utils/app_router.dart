import 'package:flutter/cupertino.dart';
import 'package:go_router/go_router.dart';

import 'package:heal_link/features/doctor/doctor_auth/presentation/views/doctor_forget_password_view.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/views/doctor_reset_password_view.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/views/doctor_sign_in_view.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/views/doctor_sign_up_second_view.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/views/doctor_sign_up_verify_email.dart';
import 'package:heal_link/features/doctor/doctor_home/data/models/add_prescription_confirm_model.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/add_prescriptions_confirm.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/add_prescriptions_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/all_patients_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/allergies_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/chronic_diseases_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/current_medications_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/doctor_home_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/past_surgeries_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/patient_view_details.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/prescription_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/total_patient_view.dart';
import 'package:heal_link/features/doctor/doctor_notifications/presentation/views/doctor_notification_view.dart';
import 'package:heal_link/features/doctor/doctor_profile/presentation/views/doctor_profile_view_body.dart';
import 'package:heal_link/features/doctor/doctor_search/presentation/views/doctor_search_view.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/personal_info.dart';
import 'package:heal_link/features/doctor/prescriptions/presentation/views/prescription.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/account_info_view.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/privacy_view.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/setting_view.dart';
import 'package:heal_link/features/on_boarding/presentation/views/on_boarding_view.dart';
import 'package:heal_link/features/on_boarding/presentation/views/user_type_screen.dart';
import 'package:heal_link/features/patient/patient_auth/presentation/views/patient_sign_in_view.dart';
import 'package:heal_link/features/patient/patient_auth/presentation/views/patient_sign_up_view.dart';
import 'package:heal_link/features/splash/presentation/views/splash_screen_view.dart';
import '../../features/doctor/doctor_auth/presentation/views/doctor_sign_up_first_view.dart';

abstract class AppRouter {
  static final GlobalKey<NavigatorState> navigatorKey =
      GlobalKey<NavigatorState>();
  static const onBoardingScreen = '/OnBoardingScreenView';
  static const userTypeScreen = '/UserTypeScreen';

  //* Doctor Views
  //? Doctor Sign Up Views
  static const doctorSignUpFirstView = '/DoctorSignUpFirstView';
  static const doctorSignUpSecondView = '/DoctorSignUpSecondView';
  static const doctorSignUpVerifyEmailView = '/DoctorSignUpVerifyEmailView';

  //? Doctor sign In Views
  static const doctorSignInView = '/DoctorSignInView';
  static const doctorSignInVerifyEmailView = '/DoctorSignInVerifyEmailView';
  static const doctorForgetPasswordView = '/DoctorForgetPasswordView';
  static const doctorResetPasswordView = '/DoctorResetPasswordView';

  static const doctorHomeView = '/DoctorHomeView';
  static const totalPatientView = '/totalPatientView';
  static const patientDetailsView = '/PatientDetailsView';
  static const prescriptions = '/AddPrescriptionView';
  static const prescriptionView = '/PrescriptionView';
  static const doctorProfileView = '/DoctorProfileView';
  static const personalInformationView = '/PersonalInformationView';
  static const addPrescriptionConfirm = '/AddPrescriptionsConfirm';
  static const doctorSearchView = '/DoctorSearchView';
  static const doctorNotificationsView = '/DoctorNotificationsView';

  static const addPrescriptionView = '/addPrescriptionView';
  static const currentMedicationsView = '/currentMedicationsView';
  static const allPatientsView = '/AllPatientsView';
  static const settingView = '/SettingView';
  static const accountInfoView = '/AccountInfoView';
  static const privacyView = '/PrivacyView';
  static const pastSurgeriesView = '/PastSurgeriesView';
  static const allergiesView = '/AllergiesView';
  static const chronicDiseasesView = '/ChronicDiseasesView';


  //* Patient Views
  static const patientSignInView = '/DoctorSignUpView';
  static const patientSignUpView = '/DoctorSignUpView';
  static const doctorSignUpView = '/DoctorSignUpView';

  static final router = GoRouter(
    navigatorKey: navigatorKey,
    routes: <RouteBase>[
      //! General Routers
      GoRoute(
        path: '/',
        builder: (BuildContext context, GoRouterState state) {
          return const SplashScreenView();
        },
      ),
      GoRoute(
        path: onBoardingScreen,
        builder: (BuildContext context, GoRouterState state) {
          return const OnBoardingView();
        },
      ),
      GoRoute(
        path: userTypeScreen,
        builder: (BuildContext context, GoRouterState state) {
          return const UserTypeScreen();
        },
      ),

      //! Doctors Routers
      //? Doctor Sign Up Views
      GoRoute(
        path: doctorSignUpFirstView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSignUpFirstView();
        },
      ),
      GoRoute(
        path: doctorSignUpSecondView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSignUpSecondView();
        },
      ),
      GoRoute(
        path: doctorSignUpVerifyEmailView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSignUpVerifyEmail();
        },
      ),

      //? Doctor sign In Views
      GoRoute(
        path: doctorSignInView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSignInView();
        },
      ),
      GoRoute(
        path: doctorForgetPasswordView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorForgetPasswordView();
        },
      ),
      GoRoute(
        path: doctorResetPasswordView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorResetPasswordView();
        },
      ),
      GoRoute(
        path: doctorProfileView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorProfileViewBody();
        },
      ),
      GoRoute(
        path: personalInformationView,
        builder: (BuildContext context, GoRouterState state) {
          return const PersonalInformationView();
        },
      ),
      GoRoute(
        path: settingView,
        builder: (BuildContext context, GoRouterState state) {
          return const SettingView();
        },
      ),
      GoRoute(
        path: accountInfoView,
        builder: (BuildContext context, GoRouterState state) {
          return const AccountInfoView();
        },
      ),
      GoRoute(
        path: privacyView,
        builder: (BuildContext context, GoRouterState state) {
          return const PrivacyView();
        },
      ),
      GoRoute(
        path: addPrescriptionView,
        builder: (BuildContext context, GoRouterState state) {
          return const AddPrescriptionsView();
        },
      ),
      GoRoute(
        path: prescriptions,
        builder: (BuildContext context, GoRouterState state) {
          return const Prescriptions();
        },
      ),

      //! Patient Routers
      GoRoute(
        path: patientSignUpView,
        builder: (BuildContext context, GoRouterState state) {
          return const PatientSignUpView();
        },
      ),
      GoRoute(
        path: patientSignInView,
        builder: (BuildContext context, GoRouterState state) {
          return const PatientSignInView();
        },
      ),
      GoRoute(
        path: doctorHomeView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorHomeView();
        },
      ),
      GoRoute(
        path: totalPatientView,
        builder: (BuildContext context, GoRouterState state) {
          return const TotalPatientView();
        },
      ),
      GoRoute(
        path: patientDetailsView,
        builder: (BuildContext context, GoRouterState state) {
          return const PatientViewDetails();
        },
      ),
      GoRoute(
        path: prescriptionView,
        builder: (BuildContext context, GoRouterState state) {
          return const PrescriptionView();
        },
      ),
      GoRoute(
        path: addPrescriptionView,
        builder: (BuildContext context, GoRouterState state) {
          return AddPrescriptionsView();
        },
      ),
      GoRoute(
        path: addPrescriptionConfirm,
        builder: (BuildContext context, GoRouterState state) {
          return AddPrescriptionsConfirm(
            addPrescriptionConfirmModel:
                state.extra as AddPrescriptionConfirmModel,
          );
        },
      ),
      GoRoute(
        path: doctorSearchView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSearchView();
        },
      ),
      GoRoute(
        path: doctorNotificationsView,
        builder: (BuildContext context, GoRouterState state) {
          return DoctorNotificationsView();
        },
      ),
      GoRoute(
        path: currentMedicationsView,
        builder: (BuildContext context, GoRouterState state) {
          return CurrentMedicationsView();
        },
      ),
      GoRoute(
        path: allPatientsView,
        builder: (BuildContext context, GoRouterState state) {
          return AllPatientsView();
        },
      ),
      GoRoute(
        path: pastSurgeriesView,
        builder: (BuildContext context, GoRouterState state) {
          return PastSurgeriesView();
        },
      ),
      GoRoute(
        path: allergiesView,
        builder: (BuildContext context, GoRouterState state) {
          return AllergiesView();
        },
      ),
      GoRoute(
        path: chronicDiseasesView,
        builder: (BuildContext context, GoRouterState state) {
          return ChronicDiseasesView();
        },
      ),
    ],
  );
}
