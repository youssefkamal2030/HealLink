import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';

class SocialButtonsRow extends StatelessWidget {
  const SocialButtonsRow({
    super.key,
    this.faceBookOnPressed,
    this.googleOnPressed,
    this.appleOnPressed,
  });
  final void Function()? faceBookOnPressed;
  final void Function()? googleOnPressed;
  final void Function()? appleOnPressed;
  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      spacing: 24,
      children: [
        IconButton(
          onPressed: faceBookOnPressed,
          icon: SvgPicture.asset(AppImages.facebook),
        ),
        IconButton(
          onPressed: googleOnPressed,
          icon: SvgPicture.asset(AppImages.google),
        ),
        IconButton(
          onPressed: appleOnPressed,
          icon: SvgPicture.asset(AppImages.apple),
        ),
      ],
    );
  }
}
