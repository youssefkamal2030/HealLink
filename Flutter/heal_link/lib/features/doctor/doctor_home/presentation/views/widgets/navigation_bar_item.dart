import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';

class NavigationBarItem extends StatelessWidget {
  const NavigationBarItem({
    super.key,
    required this.index,
    required this.image,
    required this.title,
    required this.selectedIndex,
    required this.onTap,
  });

  final int index;
  final String image;
  final String title;
  final int selectedIndex;
  final Function(int) onTap;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 56,
      height: 56,
      child: InkWell(
        splashColor: AppColors.kBackgroundColor,
        onTap: () => onTap(index),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SvgPicture.asset(
              image,
              colorFilter: ColorFilter.mode(
                selectedIndex == index
                    ? AppColors.kPrimaryColor
                    : AppColors.kDarkGreyColor,
                BlendMode.srcIn,
              ),
            ),
            Text(
              title,
              style: AppTextStyles.popins500style18LightBlackColor.copyWith(
                fontSize: 8,
                color:
                    selectedIndex == index
                        ? AppColors.kPrimaryColor
                        : AppColors.kDarkGreyColor,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
