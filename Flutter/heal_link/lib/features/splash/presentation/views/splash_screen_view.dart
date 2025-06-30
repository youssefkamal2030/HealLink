import 'package:flutter/material.dart';
import 'package:heal_link/features/splash/presentation/views/widgets/splash_screen_view_body.dart';

class SplashScreenView extends StatelessWidget {
  const SplashScreenView({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(body: SafeArea(child: SplashScreenViewBody()));
  }
}
