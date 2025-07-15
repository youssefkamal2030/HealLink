import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

import '../../../../../../generated/l10n.dart';
import 'navigation_bar_item.dart';

class CustomBottomNavBar extends StatelessWidget {
  final int selectedIndex;
  final Function(int) onTap;

  const CustomBottomNavBar({
    super.key,
    required this.selectedIndex,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: const BorderRadius.only(
        topLeft: Radius.circular(25),
        topRight: Radius.circular(25),
      ),
      child: BottomAppBar(
        color: AppColors.kWhiteColor,
        shape: CircularNotchedRectangle(),
        height: 56,
        padding: EdgeInsets.symmetric(horizontal: 15),
        child: Stack(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                NavigationBarItem(
                  image: AppImages.home,
                  index: 0,
                  title: S.of(context).home,
                  selectedIndex: selectedIndex,
                  onTap: (p0) => onTap(p0),
                ),
                NavigationBarItem(
                  image: AppImages.messageNavBar,
                  index: 1,
                  title: S.of(context).message,
                  selectedIndex: selectedIndex,
                  onTap: (p0) => onTap(p0),
                ),
                SizedBox(width: 60),
                NavigationBarItem(
                  image: AppImages.subscription,
                  index: 3,
                  title: S.of(context).subscription,
                  selectedIndex: selectedIndex,
                  onTap: (p0) => onTap(p0),
                ),
                NavigationBarItem(
                  image: AppImages.profile,
                  index: 4,
                  title: S.of(context).profile,
                  selectedIndex: selectedIndex,
                  onTap: (p0) => onTap(p0),
                ),
              ],
            ),
            Positioned(
              bottom: 12,
              right: 0,
              left: 0,
              child: ClipRRect(
                child: SvgPicture.asset(AppImages.bottomNavBar),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
