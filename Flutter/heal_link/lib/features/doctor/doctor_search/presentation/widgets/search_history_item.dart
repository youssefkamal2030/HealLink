
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class SearchHistoryItem extends StatelessWidget {
  const SearchHistoryItem({
    super.key,
    required this.searchHistory,
    required this.index,
  });
  final int index;
  final List<String> searchHistory;

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 12),
      height: 42,
      child: Row(
        children: [
          SvgPicture.asset(AppImages.clockIcon, height: 20, width: 20),
          const SizedBox(width: 8),
          Expanded(
            child: Text(
              searchHistory[index],
              style: AppTextStyles.popins400style14LightBlackColor.copyWith(
                color: AppColors.kDarkGreyColor,
              ),
            ),
          ),
          SvgPicture.asset(AppImages.arrowSearchIcon, height: 20, width: 20),
        ],
      ),
    );
  }
}
