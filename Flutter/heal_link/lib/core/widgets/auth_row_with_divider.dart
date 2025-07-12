
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/grey_horizintal_divider.dart';

class AuthRowWithDivider extends StatelessWidget {
  const AuthRowWithDivider({super.key, required this.text});
final String text ; 
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 47),
      child: Row(
        children: [
          GreyHorizintalDivider(),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 4.0),
            child: Text(
              text,
              style: AppTextStyles.popins400style14LightBlackColor.copyWith(
                color: AppColors.kDarkGreyColor,
              ),
            ),
          ),
          GreyHorizintalDivider(),
        ],
      ),
    );
  }
}
