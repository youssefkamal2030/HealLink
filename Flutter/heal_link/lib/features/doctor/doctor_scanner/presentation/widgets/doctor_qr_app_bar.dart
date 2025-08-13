
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorQrAppBar extends StatelessWidget {
  const DoctorQrAppBar({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        "Scan QR Code",
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor.copyWith(
          fontSize: 20,
        ),
      ),
    );
  }
}
