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
import 'package:heal_link/features/doctor/doctor_home/presentation/views/doctor_home_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/patient_view_details.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/prescription_view.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/total_patient_view.dart';
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
  static const prescriptionView = '/PrescriptionView';
  static const addPrescriptionView = '/addPrescriptionView';
  static const addPrescriptionConfirm = '/AddPrescriptionsConfirm';

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
          return  AddPrescriptionsView();
        },
      ),
      GoRoute(
        path: addPrescriptionConfirm,
        builder: (BuildContext context, GoRouterState state) {
          return  AddPrescriptionsConfirm(addPrescriptionConfirmModel: state.extra as AddPrescriptionConfirmModel);
        },
      ),
    ],
  );
}
