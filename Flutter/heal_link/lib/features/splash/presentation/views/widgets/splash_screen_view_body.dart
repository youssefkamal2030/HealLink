import 'package:flutter/cupertino.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';

class SplashScreenViewBody extends StatefulWidget {
  const SplashScreenViewBody({super.key});

  @override
  State<SplashScreenViewBody> createState() => _SplashScreenViewBodyState();
}

class _SplashScreenViewBodyState extends State<SplashScreenViewBody> {
  @override
  Widget build(BuildContext context) {
    return Column(children: [Text("Splash")]);
  }

  @override
  void initState() {
    super.initState();
    Future.delayed(
      Duration(seconds: 2),
      () => context.pushReplacement(AppRouter.onBoardingScreen),
    );
  }
}
