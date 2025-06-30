import 'package:flutter/cupertino.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/features/on_boarding/presentation/views/on_boarding_view.dart';
import 'package:heal_link/features/splash/presentation/views/splash_screen_view.dart';

abstract class AppRouter {
  static final GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();
  static const onBoardingScreen = '/OnBoardingScreenView';


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
    ],
  );
}
