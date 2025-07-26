
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';

class DoctorQrDoneButton extends StatelessWidget {
  const DoctorQrDoneButton({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: CustomElevatedButton(text: "Done", onPressed: () {}),
    );
  }
}
