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
    return SizedBox(
      height: 29,
      width: 74,
      child: TextButton(
        onPressed: onTap,
        style: TextButton.styleFrom(
          padding: EdgeInsets.all(0),
          backgroundColor:
              isSelected ? AppColors.kPrimaryColor : AppColors.kBackgroundColor,
          side: BorderSide(color: AppColors.kPrimaryColor,width: 1),
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        ),
        child: Text(
          label,
          style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
            color: isSelected ? AppColors.kWhiteColor : AppColors.kPrimaryColor,
          ),
        ),
      ),
    );
  }
}
