import 'package:flutter/cupertino.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/doctor_home_view.dart';
import 'package:heal_link/features/on_boarding/presentation/views/on_boarding_view.dart';
import 'package:heal_link/features/on_boarding/presentation/views/user_type_screen.dart';
import 'package:heal_link/features/splash/presentation/views/splash_screen_view.dart';
import '../../features/doctor/doctor_auth/presentation/views/doctor_sign_up_view.dart';

abstract class AppRouter {
  static final GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();
  static const onBoardingScreen = '/OnBoardingScreenView';
  static const userTypeScreen = '/UserTypeScreen';
  static const doctorSignUpView = '/DoctorSignUpView';
  static const doctorHomeView = '/DoctorHomeView';


  static final router = GoRouter(
    navigatorKey: navigatorKey,
    routes: <RouteBase>[
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
      GoRoute(
        path: doctorSignUpView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorSignUpView();
        },
      ),
      GoRoute(
        path: doctorHomeView,
        builder: (BuildContext context, GoRouterState state) {
          return const DoctorHomeView();
        },
      ),
    ],
  );
}
