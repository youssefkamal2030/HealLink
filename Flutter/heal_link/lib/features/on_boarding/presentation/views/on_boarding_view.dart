import 'package:flutter/material.dart';
import 'package:heal_link/features/on_boarding/presentation/views/widgets/on_boarding_view_body.dart';
import '../../../../core/utils/function/app_colors.dart';

class OnBoardingView extends StatelessWidget {
  const OnBoardingView({super.key});

  @override
  Widget build(BuildContext context) {
    return  Scaffold(
      backgroundColor: AppColors.kWhiteColor,
      body: OnBoardingViewBody(
      ),
    );
  }
}
