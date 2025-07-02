import 'package:flutter/material.dart';
import '../../../../../core/utils/app_images.dart';
import '../../../../../generated/l10n.dart';
import 'on_boarding_bage_view.dart';

class OnBoardingPage2 extends StatelessWidget {
  const OnBoardingPage2({super.key});

  @override
  Widget build(BuildContext context) {
    return  OnBoardingPageView(
      image: AppImages.onBoardingImage2,
      title: S.of(context).remote_consult_smart_track,
      subTitle: S.of(context).chat_send_receive,
    );
  }
}
