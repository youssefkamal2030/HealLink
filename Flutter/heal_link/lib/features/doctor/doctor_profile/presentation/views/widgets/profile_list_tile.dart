import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class ProfileListTile extends StatelessWidget {
  const ProfileListTile({super.key, required this.img, required this.title});

  final String img;
  final String title;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 56,
      padding: EdgeInsets.symmetric(horizontal: 8, vertical: 16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(8),
        boxShadow: [BoxShadow(color: Color(0x0000000D))],
      ),

      child: Row(
        spacing: 8,
        children: [
          SvgPicture.asset(img),
          Text(title, style: AppTextStyles.popins400style14LightBlackColor),
          Spacer(),
          SvgPicture.asset(
            'assets/images/arrow-right.svg',
            colorFilter: ColorFilter.mode(
              AppColors.kPrimaryColor,
              BlendMode.srcIn,
            ),
          ),
        ],
      ),
    );
  }
}
