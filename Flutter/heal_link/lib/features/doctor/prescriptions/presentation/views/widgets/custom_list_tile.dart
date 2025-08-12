import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class CustomListTile extends StatelessWidget {
  const CustomListTile({
    super.key,
    required this.img,
    required this.title,
    required this.date,
  });

  final String img;
  final String title;
  final String date;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 64,
      padding: const EdgeInsets.all(8),
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(width: 1, color: Colors.black.withOpacity(0.05)),
        borderRadius: BorderRadius.circular(8),
        boxShadow: const [BoxShadow(color: Color(0x0000000D))],
      ),
      child: Row(
        spacing: 8,
        children: [
          SvgPicture.asset(img, width: 44, height: 44),
          Text(
            title,
            style: AppTextStyles.popins400style14LightBlackColor.copyWith(
              fontSize: 16,
            ),
          ),
          const Spacer(),
          Text(date, style: AppTextStyles.popins400style10MediumGreyColor),
        ],
      ),
    );
  }
}
