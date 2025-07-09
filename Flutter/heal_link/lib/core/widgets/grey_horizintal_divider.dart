
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class GreyHorizintalDivider extends StatelessWidget {
  const GreyHorizintalDivider({super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        height: 1,
        decoration: BoxDecoration(color: AppColors.kDarkGreyColor),
      ),
    );
  }
}
