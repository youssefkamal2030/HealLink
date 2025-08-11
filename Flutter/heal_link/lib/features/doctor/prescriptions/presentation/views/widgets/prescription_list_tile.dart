import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class PrescriptionListTile extends StatelessWidget {
  const PrescriptionListTile({super.key});

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
          SvgPicture.asset(AppImages.prescriptions, width: 48, height: 48),
          Text(
            'Prescription',
            style: AppTextStyles.popins400style14LightBlackColor.copyWith(
              fontSize: 16,
            ),
          ),
          const Spacer(),
          Text(
            '02/05/2025',
            style: AppTextStyles.popins400style10MediumGreyColor,
          ),
        ],
      ),
    );
  }
}
