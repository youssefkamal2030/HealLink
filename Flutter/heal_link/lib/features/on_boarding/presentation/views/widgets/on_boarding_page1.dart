import 'package:flutter/material.dart';
import '../../../../../core/utils/app_images.dart';
import '../../../../../generated/l10n.dart';
import 'on_boarding_bage_view.dart';

class OnBoardingPage1 extends StatelessWidget {
  const OnBoardingPage1({super.key});

  @override
  Widget build(BuildContext context) {
    return OnBoardingPageView(
      image: AppImages.onBoardingImage1,
      title: S.of(context).your_health_smarter,
      subTitle: S.of(context).connect_doctor_track,
      subTitlePadding: 40,
    );
  }
}

