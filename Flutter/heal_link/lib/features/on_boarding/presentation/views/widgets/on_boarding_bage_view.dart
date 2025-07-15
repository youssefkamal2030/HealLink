import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/constant.dart';

class OnBoardingPageView extends StatelessWidget {
  const OnBoardingPageView({
    super.key,
    required this.image,
    required this.title,
    required this.subTitle,
    this.topPadding,
    this.rightPadding,
    this.subTitleTextStyle,
    this.subTitlePadding,
  });

  final String image;
  final String title;
  final String subTitle;
  final double? topPadding;
  final double? rightPadding;
  final TextStyle? subTitleTextStyle;
  final double? subTitlePadding;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Stack(
          children: [
            SizedBox(
              width: double.infinity,
              child: Image.asset(
                AppImages.onBoardingBackGround,
                height: AppConstant.height * .88,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
            ),
            Positioned(
              top: topPadding ?? AppConstant.height * .255,
              right: rightPadding ?? -10,
              child: SvgPicture.asset(image),
            ),
            Positioned(
              top: AppConstant.height * 0.64,
              right: 0,
              left: 0,
              child: Padding(
                padding: const EdgeInsets.symmetric(horizontal: 10.0),
                child: Column(
                  children: [
                    Text(
                      title,
                      textAlign: TextAlign.center,
                      style: AppTextStyles.popins700style16BlackColor,
                    ),
                    SizedBox(height: 30),
                    Padding(
                      padding: EdgeInsets.symmetric(
                        horizontal: subTitlePadding ?? 15,
                      ),
                      child: Text(
                        subTitle,
                        textAlign: TextAlign.center,
                        style:
                            subTitleTextStyle ??
                            AppTextStyles.popins400style14LightBlackColor,
                      ),
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
