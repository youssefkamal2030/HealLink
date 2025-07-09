
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomBottomHintOfTextFeildPart extends StatelessWidget {
  const CustomBottomHintOfTextFeildPart({
    super.key,
    required this.text , 
  });
final String text ; 
  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: AppTextStyles.popins400style14LightBlackColor.copyWith(
        color: AppColors.kGreyColor,
      ),
    );
  }
}
