
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomElevatedButton extends StatelessWidget {
  const CustomElevatedButton({super.key, required this.text, this.onPressed, this.backGroundColor, this.textColor});
  final String text;
  final void Function()? onPressed;
  final Color? backGroundColor ; 
  final Color? textColor ; 
  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: double.infinity,
      child: ElevatedButton(
        style: ElevatedButton.styleFrom(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadiusGeometry.circular(8),
          ),
          backgroundColor: backGroundColor ??  AppColors.kPrimaryColor  ,
        ),
        onPressed: onPressed,
        child: Text(
          text,
          style: AppTextStyles.popins500style18LightBlackColor.copyWith(
            color: textColor ?? AppColors.kWhiteColor,
          ),
        ),
      ),
    );
  }
}
