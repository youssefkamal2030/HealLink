
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class AuthFooter extends StatelessWidget {
  const AuthFooter({super.key, required this.text1, required this.text2, this.onTap});
final String text1 ; 
final String text2 ; 
final void Function()? onTap ;
  @override
  Widget build(BuildContext context) {
    return Row(
      spacing: 4,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Text(
          text1,
          style: AppTextStyles.popins400style14LightBlackColor,
        ),

        GestureDetector(
          onTap: onTap,
          child: Text(
            text2,
            style: AppTextStyles.popins400style14LightBlackColor.copyWith(
              color: AppColors.kPrimaryColor,
            ),
          ),
        ),
      ],
    );
  }
}
