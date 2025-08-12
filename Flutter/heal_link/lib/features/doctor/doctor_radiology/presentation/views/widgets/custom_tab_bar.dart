import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomTabBar extends StatefulWidget {
  final List<String> tabs;
  final ValueChanged<int>? onTabSelected;

  const CustomTabBar({super.key, required this.tabs, this.onTabSelected});

  @override
  State<CustomTabBar> createState() => _CustomTabBarState();
}

class _CustomTabBarState extends State<CustomTabBar> {
  int selectedIndex = 0;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      scrollDirection: Axis.horizontal,
      child: Row(
        children: List.generate(widget.tabs.length, (index) {
          final bool isSelected = selectedIndex == index;
          return InkWell(
            onTap: () {
              setState(() {
                selectedIndex = index;
              });
              if (widget.onTabSelected != null) {
                widget.onTabSelected!(index);
              }
            },
            child: Container(
              height: 26,
              margin: const EdgeInsets.symmetric(horizontal: 4),
              padding: const EdgeInsets.symmetric(horizontal: 8),
              decoration: BoxDecoration(
                color: isSelected ? AppColors.kPrimaryColor : Colors.white,
                borderRadius: BorderRadius.circular(8),
              ),
              alignment: Alignment.center,
              child: Text(
                widget.tabs[index],
                style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                  color:
                      isSelected
                          ? AppColors.kWhiteColor
                          : AppColors.kLightBlackColor,
                ),
              ),
            ),
          );
        }),
      ),
    );
  }
}
