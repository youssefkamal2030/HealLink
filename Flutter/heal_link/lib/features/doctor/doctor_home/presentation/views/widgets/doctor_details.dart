import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/app_styles.dart';
import 'custom_circle_image.dart';

class DoctorDetails extends StatelessWidget {
  const DoctorDetails({
    super.key,
    required this.image,
    required this.name,
    required this.type,
    required this.starNo,
    required this.patientNo,
  });

  final String image;
  final String name;
  final String type;
  final double starNo;
  final int patientNo;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        CustomCircleImage(image: image, isVerified: true, radius: 31),
        SizedBox(width: 8),
        Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 4,
          children: [
            Text(name, style: AppTextStyles.popins500style18LightBlackColor),
            Container(
              padding: EdgeInsets.symmetric(horizontal: 26, vertical: 1),
              decoration: BoxDecoration(
                color: Color(0x1A2E8EF5),
                borderRadius: BorderRadius.circular(4),
              ),
              child: Text(
                type,
                style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
                  color: Color(0xff2E8EF5),
                ),
              ),
            ),
            Row(
              children: [
                Row(
                  children: List.generate(
                    5,
                    (index) => Padding(
                      padding: const EdgeInsets.only(right: 3),
                      child: SvgPicture.asset(
                        AppImages.star,
                        width: 10,
                        height: 10,
                      ),
                    ),
                  ),
                ),
                Text(
                  '$starNo ($patientNo)',
                  style: AppTextStyles.popins400style10MediumGreyColor,
                ),
              ],
            ),
            SizedBox(height: 0.5),
          ],
        ),
      ],
    );
  }
}
