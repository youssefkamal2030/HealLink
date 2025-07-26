import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomFullButton extends StatelessWidget {
  final String text;
  final VoidCallback onPressed;
  final double height;
  final double? radios;
  final TextStyle? textStyle;

  const CustomFullButton({
    super.key,
    required this.text,
    required this.onPressed,
    this.height = 50,
    this.radios,
    this.textStyle,
  });

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      height: height,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(radios ?? 30),
      ),
      minWidth: double.infinity,
      color: AppColors.kPrimaryColor,
      onPressed: onPressed,
      child: Text(
        text,
        style:textStyle?? AppTextStyles.popins500style18LightBlackColor.copyWith(
          color: AppColors.kWhiteColor,
        ),
      ),
    );
  }
}
