import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomTabButton extends StatelessWidget {
  final String label;
  final bool isSelected;
  final VoidCallback onTap;

  const CustomTabButton({
    super.key,
    required this.label,
    required this.isSelected,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return TextButton(
      onPressed: onTap,
      style: TextButton.styleFrom(
        backgroundColor:
            isSelected ? AppColors.kPrimaryColor : AppColors.kWhiteColor,
        side: BorderSide(color: AppColors.kPrimaryColor),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      ),
      child: Text(
        label,
        style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
          color: isSelected ? AppColors.kWhiteColor : AppColors.kPrimaryColor,
        ),
      ),
    );
  }
}
