import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

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
        notchMargin: 12,
        padding: EdgeInsets.symmetric(horizontal: 15),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            NavigationBarItem(
              image: AppImages.home,
              index: 0,
              title: 'Home',
              selectedIndex: selectedIndex,
              onTap: (p0) => onTap(p0),
            ),
            NavigationBarItem(
              image: AppImages.message,
              index: 1,
              title: 'Message',
              selectedIndex: selectedIndex,
              onTap: (p0) => onTap(p0),
            ),
            SizedBox(width: 55),
            NavigationBarItem(
              image: AppImages.subscription,
              index: 3,
              title: 'Subscription',
              selectedIndex: selectedIndex,
              onTap: (p0) => onTap(p0),
            ),
            NavigationBarItem(
              image: AppImages.profile,
              index: 4,
              title: 'Profile',
              selectedIndex: selectedIndex,
              onTap: (p0) => onTap(p0),
            ),
          ],
        ),
      ),
    );
  }
}
