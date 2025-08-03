import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class SettingField extends StatelessWidget {
  const SettingField({super.key, required this.label, this.onTap});

  final String label;
  final void Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Column(
        children: [
          Container(
            height: 30,
            padding: EdgeInsets.all(4.5),
            decoration: BoxDecoration(
              border: Border(
                bottom: BorderSide(
                  width: 1,
                  color: Color.fromARGB(0, 0, 0, 0).withOpacity(0.05),
                ),
              ),
            ),
            child: Row(
              children: [
                Text(
                  label,
                  style: AppTextStyles.popins400style14LightBlackColor,
                ),
                Spacer(),
                SvgPicture.asset(AppImages.arrow, height: 20, width: 20),
              ],
            ),
          ),
          SizedBox(height: 8),
        ],
      ),
    );
  }
}
