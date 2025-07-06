import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/function/app_colors.dart';

class ScannerWidget extends StatelessWidget {
  const ScannerWidget({
    super.key,
    required this.onPressed,
    required this.selectedIndex,
  });

  final void Function() onPressed;
  final int selectedIndex;

  @override
  Widget build(BuildContext context) {
    return FloatingActionButton(
      backgroundColor: AppColors.kWhiteColor,
      onPressed: onPressed,
      shape: CircleBorder(),
      elevation: 0,
      child: SvgPicture.asset(
        AppImages.scanner,
        colorFilter: ColorFilter.mode(
          selectedIndex == 2
              ? AppColors.kPrimaryColor
              : AppColors.kDarkGreyColor,
          BlendMode.srcIn,
        ),
      ),
    );
  }
}
