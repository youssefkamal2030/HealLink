import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/constant.dart';
import '../../../../../core/utils/app_images.dart';

class OnBoardingPageView extends StatelessWidget {
  const OnBoardingPageView({
    super.key,
    required this.image,
    required this.title,
    required this.subTitle,
  });

  final String image;
  final String title;
  final String subTitle;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Stack(
          children: [
            SizedBox(
              width: double.infinity,
              child: SvgPicture.asset(
                AppImages.onBoardingBackGround,
                height: AppConstant.height * .85,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
            ),
            Positioned(
              top: AppConstant.height * .27,
              left: 46,
              child: SvgPicture.asset(image),
            ),
            Positioned(
              top: AppConstant.height * 0.64,
              right: 0,
              left: 0,
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 20.0),
                child: Column(
                  children: [
                    Text(title, textAlign: TextAlign.center),
                    SizedBox(height: 30),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 36),
                      child: Text(subTitle, textAlign: TextAlign.center),
                    ),
                    SizedBox(height: 56),
                  ],
                ),
              ),
            ),
          ],
        ),
      ],
    );
  }
}
